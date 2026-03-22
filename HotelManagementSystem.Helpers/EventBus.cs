using System.Windows.Forms;

namespace hotel_management_system.Helpers
{
    /// <summary>
    /// Простий EventBus для міжформової комунікації.
    /// </summary>
    public static class EventBus
    {
        // ---------- Типізовані події ----------
        public static event Action RoomsChanged;
        public static event Action<int> BookingCreated;

        // ---------- Топікова шина ----------
        private static readonly object _lock = new();
        private static readonly Dictionary<string, List<Subscription>> _subs = new();

        private sealed class Subscription
        {
            public string Topic { get; init; }
            public Action<object> Handler { get; init; }
            public SynchronizationContext Context { get; init; }
            public WeakReference<Form> OwnerForm { get; init; }
            public Guid Id { get; } = Guid.NewGuid();
        }

        private sealed class SubscriptionToken : IDisposable
        {
            private readonly string _topic;
            private readonly Guid _id;
            private bool _disposed;

            public SubscriptionToken(string topic, Guid id)
            {
                _topic = topic;
                _id = id;
            }

            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;
                Unsubscribe(_topic, _id);
            }
        }

        /// <summary>
        /// Підписка на топік. Якщо передати owner (Form) – буде авто-відписка при FormClosed.
        /// </summary>
        public static IDisposable Subscribe(string topic, Action<object> handler, Form owner = null)
        {
            if (string.IsNullOrWhiteSpace(topic)) throw new ArgumentNullException(nameof(topic));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var sub = new Subscription
            {
                Topic = topic,
                Handler = handler,
                Context = SynchronizationContext.Current,
                OwnerForm = owner != null ? new WeakReference<Form>(owner) : null
            };

            lock (_lock)
            {
                if (!_subs.TryGetValue(topic, out var list))
                {
                    list = new List<Subscription>();
                    _subs[topic] = list;
                }
                list.Add(sub);
            }

            if (owner != null)
            {
                owner.FormClosed += (_, __) => Unsubscribe(topic, sub.Id);
            }

            return new SubscriptionToken(topic, sub.Id);
        }

        /// <summary>Публікація події у топік.</summary>
        public static void Publish(string topic, object payload = null)
        {
            List<Subscription> targets;
            lock (_lock)
            {
                if (!_subs.TryGetValue(topic, out var list) || list.Count == 0)
                    targets = new List<Subscription>();
                else
                    targets = list.ToList(); // копія
            }

            // Міст до типізованих подій
            if (string.Equals(topic, "RoomsChanged", StringComparison.OrdinalIgnoreCase))
                RoomsChanged?.Invoke();

            if (string.Equals(topic, "BookingCreated", StringComparison.OrdinalIgnoreCase))
            {
                if (payload is int idInt)
                    BookingCreated?.Invoke(idInt);
            }

            foreach (var sub in targets)
            {
                if (sub.OwnerForm != null &&
                    (!sub.OwnerForm.TryGetTarget(out var f) || f.IsDisposed))
                {
                    Unsubscribe(topic, sub.Id);
                    continue;
                }

                if (sub.Context != null)
                    sub.Context.Post(_ => SafeInvoke(sub.Handler, payload), null);
                else
                    SafeInvoke(sub.Handler, payload);
            }
        }

        private static void Unsubscribe(string topic, Guid id)
        {
            lock (_lock)
            {
                if (_subs.TryGetValue(topic, out var list))
                {
                    list.RemoveAll(s => s.Id == id);
                    if (list.Count == 0) _subs.Remove(topic);
                }
            }
        }

        private static void SafeInvoke(Action<object> handler, object payload)
        {
            try { handler(payload); }
            catch
            {
                // за бажанням можна залогувати
            }
        }

        // ---------- Зворотна сумісність зі старими викликами ----------
        public static void RaiseRoomsChanged() => Publish("RoomsChanged");

        public static void RaiseBookingCreated(int bookingId)
        {
            BookingCreated?.Invoke(bookingId);
            Publish("BookingCreated", bookingId);
        }

        // Зручні типізовані підписки
        public static IDisposable SubscribeRoomsChanged(Action handler, Form owner = null)
            => Subscribe("RoomsChanged", _ => handler(), owner);

        public static IDisposable SubscribeBookingCreated(Action<int> handler, Form owner = null)
            => Subscribe("BookingCreated", o =>
            {
                if (o is int id) handler(id);
            }, owner);
    }
}
