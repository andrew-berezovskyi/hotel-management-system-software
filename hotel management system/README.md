🏨 Hotel Management System
Windows Forms · C# (.NET 8) · SQLite · ReaLTaiizor UI

Це повнофункціональна десктопна система для управління готелем, розроблена на C# (.NET 8, Windows Forms) із використанням SQLite, розширеного UI (ReaLTaiizor), та модульної архітектури.

Система підтримує два типи користувачів:

Адміністратор — керування готелем, користувачами, кімнатами, звітами, бронюваннями, оплатами, персоналом.

Гість — перегляд кімнат, бронювання номерів, управління власним кабінетом.

📁 Структура проєкту
Solution: hotel management system (7 projects)
├─ HotelManagementSystem (WinForms UI)
│  ├─ App/
│  │  └─ Program.cs                      → Вхід у застосунок, запуск першої форми
│  ├─ Forms/
│  │  ├─ Admin/
│  │  │  ├─ AdminDashboardForm.cs        → Головна панель адміна + голосове керування
│  │  │  ├─ AdminSetupForm.cs            → Створення першого адміністратора (ініціалізація)
│  │  │  ├─ HotelInfoForm.cs             → Дані про готель (назва/адреса/фото)
│  │  │  ├─ ManageRoomsForm.cs           → CRUD (Create, Read, Update, Delete) номерів                       
│  │  │  ├─ ManageUsersForm.cs           → CRUD користувачів
│  │  │  ├─ ManageStaffForm.cs           → CRUD персоналу
│  │  │  ├─ ManageBookingsForm.cs        → CRUD бронювань + статуси
│  │  │  ├─ ManagePaymentsForm.cs        → CRUD оплат
│  │  │  ├─ ManageReportsForm.cs         → Звіти (дохід/завантаженість тощо)
│  │  │  └─ SystemSettingsForm.cs        → Налаштування системи
│  │  ├─ Auth/
│  │  │  ├─ LoginForm.cs                 → Логін + сесія AuthHelper + навігація
│  │  │  ├─ RegistrationForm.cs          → Реєстрація (UserService + валідації)
│  │  │  └─ ResetPasswordForm.cs         → Скидання пароля (код/OTP)
│  │  ├─ Shared/
│  │  │  └─ WelcomeForm.cs               → Стартовий екран (welcome_bg_blur)
│  │  └─ User/
│  │     ├─ UserDashboardForm.cs         → Головний екран гостя + навігація
│  │     ├─ UserRoomsForm.cs             → Список кімнат (картки) + фільтр
│  │     ├─ UserBookingForm.cs           → Бронювання кімнати (перевірка доступності)
│  │     ├─ UserAccountForm.cs           → Кабінет (бронювання користувача)
│  │     └─ UserContactsForm.cs          → Контакти готелю
│  ├─ UIHelpers/
│  │  ├─ NavigationHelper.cs             → Перемикання форм / logout / back to dashboard
│  │  ├─ FormAnimationHelper.cs          → Fade-in анімація для форм
│  │  ├─ BookingDialogHelper.cs          → Діалог Add/Edit Booking (адмінська частина)
│  │  ├─ RoomDialogHelper.cs             → Діалог Add/Edit Room
│  │  ├─ PaymentDialogHelper.cs          → Діалог Add/Edit Payment
│  │  ├─ RoomPresentationHelper.cs       → Картинка/опис/highlights/services для типу кімнати
│  │  └─ GlassPanelHelper.cs             → (з твого солюшену) UI-ефекти/панелі (якщо юзаєш)
│  ├─ Resources/
│  │  ├─ appsettings.json                → Конфіг (БД/SMTP/назва готелю тощо)
│  │  ├─ *.jpg (room_*.jpg)              → Картинки кімнат (standard/superior/…)
│  │  ├─ welcome_bg_blur.png             → Фон Welcome
│  │  ├─ no_image.png                    → Плейсхолдер
│  │  └─ Resources.resx / hotel.ico      → Ресурси/іконка
│  └─ README.md                          → Документація 
│
├─ HotelManagementSystem.Services         → Бізнес-логіка (CRUD + правила)
│  ├─ BookingService.cs                  → CRUD бронювань + автозміна статусів + availability
│  ├─ RoomService.cs                     → CRUD кімнат + availability
│  ├─ UserService.cs                     → Реєстрація/логін/CRUD + reset-code
│  ├─ PaymentService.cs                  → CRUD оплат
│  ├─ StaffService.cs                    → CRUD персоналу
│  ├─ ReportService.cs                   → Побудова звітів
│  ├─ ContactService.cs                  → Дані контактів
│  ├─ EmailService.cs                    → Відправка email (OTP/повідомлення)
│  ├─ EmailTestService.cs                → Перевірка SMTP
│  └─ DemoDataSeeder.cs                  → Генерація тестових даних
│
├─ HotelManagementSystem.Data             → SQLite доступ + SQL
│  ├─ DbHelper.cs                        → ExecuteQuery/NonQuery/Scalar (низький рівень)
│  ├─ DatabaseInitializer.cs             → Ініціалізація БД/таблиць (якщо є)
│  └─ SqlQueries/
│     ├─ UserQueries.cs                  → SQL для Users
│     ├─ RoomQueries.cs                  → SQL для Rooms
│     ├─ BookingQueries.cs               → SQL для Bookings (+ авто-апдейт)
│     ├─ PaymentQueries.cs               → SQL для Payments
│     ├─ StaffQueries.cs                 → SQL для Staff
│     └─ ReportQueries.cs                → SQL для Reports
│
├─ HotelManagementSystem.Helpers          → Утиліти (сесія/валідація/лог/тости/конфіг)
│  ├─ AuthHelper.cs                      → Поточна сесія (CurrentUser/IsAdmin/Logout/Refresh)
│  ├─ ConfigHelper.cs                    → Читання appsettings.json
│  ├─ EventBus.cs                        → Події між формами (RoomsChanged/BookingCreated…)
│  ├─ ToastHelper.cs                     → UI сповіщення (Success/Info/Warning/Error)
│  ├─ LoggerHelper.cs                    → Логи + LogException
│  ├─ ValidationHelper.cs                → Перевірки (порожнє/емейл/телефон…)
│  ├─ PasswordHelper.cs                  → Hash/Verify паролів
│  ├─ RoomOptions.cs                     → Довідники: типи/статуси кімнат
│  ├─ RoomHelper.cs                      → Додаткові перетворення/валідації номерів
│  ├─ ExcelExportHelper.cs               → Експорт таблиць (CSV/Excel)
│  ├─ StaffDialogHelper.cs               → Діалог персоналу (якщо винесено в helper)
│  ├─ UserDialogHelper.cs                → Діалог користувачів
│  └─ SystemSettingsHelper.cs            → Збереження/читання системних налаштувань
│
├─ HotelManagementSystem.Models           → Моделі (DTO/Entity)
│  ├─ User.cs                            → Користувач (FullName/Username/Email/…/IsAdmin)
│  ├─ Room.cs                            → Кімната (Id/Number/Type/Price/Status/ImagePath)
│  ├─ Booking.cs                         → Бронювання (UserId/RoomId/дати/TotalPrice/…)
│  ├─ Payment.cs                         → Оплата (BookingId/Amount/Date/Method/Status/Notes)
│  ├─ Staff.cs                           → Персонал
│  └─ ReportSummary.cs                   → Підсумок звіту
│
└─ HotelManagementSystem.Voice            → Голосові команди (адмін-дашборд)
   ├─ VoiceCommandService.cs             → Start/Stop + розпізнавання + івенти
   └─ VoiceCommandDetectedEventArgs.cs   → EventArgs з типом команди/текстом


🧩 Функціональні можливості
👤 Користувач:

Перегляд доступних кімнат (фільтр, фото, опис).

Бронювання номера з перевіркою доступності.

Перегляд своїх бронювань.

Скасування бронювання.

Перегляд контактів готелю.

⚙️ Адміністратор:

Повноцінні CRUD панелі:
✔ Користувачі
✔ Кімнати
✔ Персонал
✔ Бронювання
✔ Оплати

Система звітів:
✔ Доходи
✔ Заповненість
✔ Популярні кімнати

Системні налаштування (email, OTP, шлях БД).

Резервне копіювання БД.

Генерація тестових даних.

🧠 Архітектура проекту
🔶 Модель – Сервіс – Презентація (WinForms)

Models/ — всі сутності (User, Room, Booking…).

Services/ — бізнес-логіка, валідатори, CRUD.

Forms/ — UI рівень.

Data/ — SQL-запити + SQLite.

Helpers/ — утиліти, навігація, логування, email, хелпери діалогів.

🔶 Подієва модель (EventBus)

Форми оновлюють одна одну без перезавантажень.

🔶 Конфігурація в appsettings.json

Порт SMTP

Email

Шлях до БД

Назва готелю

Час дії OTP

🔶 Безпека

Паролі зберігаються у вигляді SHA256-хешу.

Валідація всіх даних.

Перевірка доступності кімнат.

Уникнення дублювання бронювань.

🗄️ База даних SQLite
Таблиці:

Users

Rooms

Bookings

Payments

Staff

HotelInfo

База створюється автоматично при першому запуску.

🚀 Як запустити проєкт

Відкрити HotelManagementSystem.sln у Visual Studio.

Перевірити NuGet пакети:

System.Data.SQLite

Microsoft.Extensions.Configuration

ReaLTaiizor

Переконатися, що файл appsettings.json існує.

Натиснути F5.

При першому запуску система попросить створити первинного адміністратора.

👨‍💻 Автор

Березовський Андрій
Hotel Management System · 2025
C# / WinForms / SQLite / ReaLTaiizor