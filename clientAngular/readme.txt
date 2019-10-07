ng new walletApplication
-- Створення нового додатку

ng serve -o
-- Запуск проекту (-о відкривається браузер)

npm install bootstrap
-- Установка bootstrap

npm install
-- Установлює всі пакети прописані в package.json

ng g(generate) module spendings

-- створить модуль SpendingsModule

ng g(generate) component SpendingItem

-- Створить компоненту SpendingItemComponent\

не бачить ngModel то треба подивитися чи підключений FormsModule до модуля де реєструється ця компонента

не робить роутерлінк значить треба підключити RouterModule

