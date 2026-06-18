PRAGMA foreign_keys = ON;
BEGIN TRANSACTION;
INSERT INTO roles (name) VALUES ('Авторизованный клиент');
INSERT INTO roles (name) VALUES ('Администратор');
INSERT INTO roles (name) VALUES ('Менеджер');
INSERT INTO categories (name) VALUES ('Защита лица, глаз, головы');
INSERT INTO categories (name) VALUES ('Не указано');
INSERT INTO categories (name) VALUES ('Общестроительные материалы');
INSERT INTO categories (name) VALUES ('Ручной инструмент');
INSERT INTO categories (name) VALUES ('Стеновые и фасадные материалы');
INSERT INTO categories (name) VALUES ('Сухие строительные смеси и гидроизоляция');
INSERT INTO units (name) VALUES ('шт.');
INSERT INTO suppliers (name) VALUES ('Armero');
INSERT INTO suppliers (name) VALUES ('Delta');
INSERT INTO suppliers (name) VALUES ('Hesler');
INSERT INTO suppliers (name) VALUES ('Husqvarna');
INSERT INTO suppliers (name) VALUES ('KILIMGRIN');
INSERT INTO suppliers (name) VALUES ('Knauf');
INSERT INTO suppliers (name) VALUES ('MixMaster');
INSERT INTO suppliers (name) VALUES ('RUIZ');
INSERT INTO suppliers (name) VALUES ('Vinylon');
INSERT INTO suppliers (name) VALUES ('Weber');
INSERT INTO suppliers (name) VALUES ('Wenzo Roma');
INSERT INTO suppliers (name) VALUES ('ВОЛМА');
INSERT INTO suppliers (name) VALUES ('Изостронг');
INSERT INTO suppliers (name) VALUES ('Исток');
INSERT INTO suppliers (name) VALUES ('ЛСР');
INSERT INTO suppliers (name) VALUES ('М500');
INSERT INTO suppliers (name) VALUES ('Не указан');
INSERT INTO suppliers (name) VALUES ('Павловский завод');
INSERT INTO manufacturers (name) VALUES ('Armero');
INSERT INTO manufacturers (name) VALUES ('Delta');
INSERT INTO manufacturers (name) VALUES ('Hesler');
INSERT INTO manufacturers (name) VALUES ('Husqvarna');
INSERT INTO manufacturers (name) VALUES ('KILIMGRIN');
INSERT INTO manufacturers (name) VALUES ('Knauf');
INSERT INTO manufacturers (name) VALUES ('MixMaster');
INSERT INTO manufacturers (name) VALUES ('RUIZ');
INSERT INTO manufacturers (name) VALUES ('Vinylon');
INSERT INTO manufacturers (name) VALUES ('Weber');
INSERT INTO manufacturers (name) VALUES ('Wenzo Roma');
INSERT INTO manufacturers (name) VALUES ('ВОЛМА');
INSERT INTO manufacturers (name) VALUES ('Изостронг');
INSERT INTO manufacturers (name) VALUES ('Исток');
INSERT INTO manufacturers (name) VALUES ('ЛСР');
INSERT INTO manufacturers (name) VALUES ('М500');
INSERT INTO manufacturers (name) VALUES ('Не указан');
INSERT INTO manufacturers (name) VALUES ('Павловский завод');
INSERT INTO order_statuses (name) VALUES ('Завершен');
INSERT INTO order_statuses (name) VALUES ('Новый');
INSERT INTO pickup_points (id, address) VALUES (1, '420151, г. Лесной, ул. Вишневая, 32');
INSERT INTO pickup_points (id, address) VALUES (2, '125061, г. Лесной, ул. Подгорная, 8');
INSERT INTO pickup_points (id, address) VALUES (3, '630370, г. Лесной, ул. Шоссейная, 24');
INSERT INTO pickup_points (id, address) VALUES (4, '400562, г. Лесной, ул. Зеленая, 32');
INSERT INTO pickup_points (id, address) VALUES (5, '614510, г. Лесной, ул. Маяковского, 47');
INSERT INTO pickup_points (id, address) VALUES (6, '410542, г. Лесной, ул. Светлая, 46');
INSERT INTO pickup_points (id, address) VALUES (7, '620839, г. Лесной, ул. Цветочная, 8');
INSERT INTO pickup_points (id, address) VALUES (8, '443890, г. Лесной, ул. Коммунистическая, 1');
INSERT INTO pickup_points (id, address) VALUES (9, '603379, г. Лесной, ул. Спортивная, 46');
INSERT INTO pickup_points (id, address) VALUES (10, '603721, г. Лесной, ул. Гоголя, 41');
INSERT INTO pickup_points (id, address) VALUES (11, '410172, г. Лесной, ул. Северная, 13');
INSERT INTO pickup_points (id, address) VALUES (12, '614611, г. Лесной, ул. Молодежная, 50');
INSERT INTO pickup_points (id, address) VALUES (13, '454311, г.Лесной, ул. Новая, 19');
INSERT INTO pickup_points (id, address) VALUES (14, '660007, г.Лесной, ул. Октябрьская, 19');
INSERT INTO pickup_points (id, address) VALUES (15, '603036, г. Лесной, ул. Садовая, 4');
INSERT INTO pickup_points (id, address) VALUES (16, '394060, г.Лесной, ул. Фрунзе, 43');
INSERT INTO pickup_points (id, address) VALUES (17, '410661, г. Лесной, ул. Школьная, 50');
INSERT INTO pickup_points (id, address) VALUES (18, '625590, г. Лесной, ул. Коммунистическая, 20');
INSERT INTO pickup_points (id, address) VALUES (19, '625683, г. Лесной, ул. 8 Марта');
INSERT INTO pickup_points (id, address) VALUES (20, '450983, г.Лесной, ул. Комсомольская, 26');
INSERT INTO pickup_points (id, address) VALUES (21, '394782, г. Лесной, ул. Чехова, 3');
INSERT INTO pickup_points (id, address) VALUES (22, '603002, г. Лесной, ул. Дзержинского, 28');
INSERT INTO pickup_points (id, address) VALUES (23, '450558, г. Лесной, ул. Набережная, 30');
INSERT INTO pickup_points (id, address) VALUES (24, '344288, г. Лесной, ул. Чехова, 1');
INSERT INTO pickup_points (id, address) VALUES (25, '614164, г.Лесной,  ул. Степная, 30');
INSERT INTO pickup_points (id, address) VALUES (26, '394242, г. Лесной, ул. Коммунистическая, 43');
INSERT INTO pickup_points (id, address) VALUES (27, '660540, г. Лесной, ул. Солнечная, 25');
INSERT INTO pickup_points (id, address) VALUES (28, '125837, г. Лесной, ул. Шоссейная, 40');
INSERT INTO pickup_points (id, address) VALUES (29, '125703, г. Лесной, ул. Партизанская, 49');
INSERT INTO pickup_points (id, address) VALUES (30, '625283, г. Лесной, ул. Победы, 46');
INSERT INTO pickup_points (id, address) VALUES (31, '614753, г. Лесной, ул. Полевая, 35');
INSERT INTO pickup_points (id, address) VALUES (32, '426030, г. Лесной, ул. Маяковского, 44');
INSERT INTO pickup_points (id, address) VALUES (33, '450375, г. Лесной ул. Клубная, 44');
INSERT INTO pickup_points (id, address) VALUES (34, '625560, г. Лесной, ул. Некрасова, 12');
INSERT INTO pickup_points (id, address) VALUES (35, '630201, г. Лесной, ул. Комсомольская, 17');
INSERT INTO pickup_points (id, address) VALUES (36, '190949, г. Лесной, ул. Мичурина, 26');
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Ворсин Петр Евгеньевич', '94d5ous@gmail.com', 'uzWC67' FROM roles WHERE name = 'Администратор';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Старикова Елена Павловна', 'uth4iz@mail.com', '2L6KZG' FROM roles WHERE name = 'Администратор';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Одинцов Серафим Артёмович', 'yzls62@outlook.com', 'JlFRCZ' FROM roles WHERE name = 'Администратор';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Степанов Михаил Артёмович', '1diph5e@tutanota.com', '8ntwUp' FROM roles WHERE name = 'Менеджер';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Ворсин Петр Евгеньевич', 'tjde7c@yahoo.com', 'YOyhfR' FROM roles WHERE name = 'Менеджер';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Старикова Елена Павловна', 'wpmrc3do@tutanota.com', 'RSbvHv' FROM roles WHERE name = 'Менеджер';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Михайлюк Анна Вячеславовна', '5d4zbu@tutanota.com', 'rwVDh9' FROM roles WHERE name = 'Авторизованный клиент';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Ситдикова Елена Анатольевна', 'ptec8ym@yahoo.com', 'LdNyos' FROM roles WHERE name = 'Авторизованный клиент';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Никифорова Весения Николаевна', '1qz4kw@mail.com', 'gynQMT' FROM roles WHERE name = 'Авторизованный клиент';
INSERT INTO users (role_id, full_name, login, password) SELECT id, 'Сазонов Руслан Германович', '4np6se@mail.com', 'AtnDjr' FROM roles WHERE name = 'Авторизованный клиент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'PMEZMH', 'Цемент', units.id, 440.00, suppliers.id, manufacturers.id, categories.id, 8, 34, 'Цемент Евроцемент М500 Д0 ЦЕМ I 42,5 50 кг', 'PMEZMH.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'М500' AND manufacturers.name = 'М500' AND categories.name = 'Общестроительные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'BPV4MM', 'Пленка техническая', units.id, 8.00, suppliers.id, manufacturers.id, categories.id, 8, 2, 'Пленка техническая полиэтиленовая Изостронг 60 мк 3 м рукав 1,5 м, пог.м', 'BPV4MM.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Изостронг' AND manufacturers.name = 'Изостронг' AND categories.name = 'Общестроительные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'JVL42J', 'Пленка техническая', units.id, 13.00, suppliers.id, manufacturers.id, categories.id, 4, 34, 'Пленка техническая полиэтиленовая Изостронг 100 мк 3 м рукав 1,5 м, пог.м', 'JVL42J.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Изостронг' AND manufacturers.name = 'Изостронг' AND categories.name = 'Общестроительные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'F895RB', 'Песок строительный', units.id, 102.00, suppliers.id, manufacturers.id, categories.id, 6, 7, 'Песок строительный 50 кг', 'F895RB.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Knauf' AND manufacturers.name = 'Knauf' AND categories.name = 'Общестроительные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '3XBOTN', 'Керамзит фракция', units.id, 110.00, suppliers.id, manufacturers.id, categories.id, 5, 21, 'Керамзит фракция 10-20 мм 0,05 куб.м', '3XBOTN.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'MixMaster' AND manufacturers.name = 'MixMaster' AND categories.name = 'Общестроительные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '3L7RCZ', 'Газобетон', units.id, 7400.00, suppliers.id, manufacturers.id, categories.id, 2, 20, 'Газобетон ЛСР 100х250х625 мм D400', '3L7RCZ.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'ЛСР' AND manufacturers.name = 'ЛСР' AND categories.name = 'Стеновые и фасадные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'S72AM3', 'Пазогребневая плита', units.id, 500.00, suppliers.id, manufacturers.id, categories.id, 5, 35, 'Пазогребневая плита ВОЛМА Гидро 667х500х80 мм полнотелая', 'S72AM3.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'ВОЛМА' AND manufacturers.name = 'ВОЛМА' AND categories.name = 'Стеновые и фасадные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '2G3280', 'Угол наружный', units.id, 795.00, suppliers.id, manufacturers.id, categories.id, 9, 20, 'Угол наружный Vinylon 3050 мм серо-голубой', '2G3280.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Vinylon' AND manufacturers.name = 'Vinylon' AND categories.name = 'Стеновые и фасадные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'MIO8YV', 'Кирпич', units.id, 30.00, suppliers.id, manufacturers.id, categories.id, 9, 31, 'Кирпич рядовой Боровичи полнотелый М150 250х120х65 мм 1NF', 'MIO8YV.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'ВОЛМА' AND manufacturers.name = 'ВОЛМА' AND categories.name = 'Стеновые и фасадные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'UER2QD', 'Скоба для пазогребневой плиты', units.id, 25.00, suppliers.id, manufacturers.id, categories.id, 8, 27, 'Скоба для пазогребневой плиты Knauf С1 120х100 мм', 'UER2QD.jpg'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Knauf' AND manufacturers.name = 'Knauf' AND categories.name = 'Стеновые и фасадные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'ZR70B4', 'Кирпич', units.id, 16.00, suppliers.id, manufacturers.id, categories.id, 3, 0, 'Кирпич рядовой силикатный Павловский завод полнотелый М200 250х120х65 мм 1NF', 'ZR70B4.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Павловский завод' AND manufacturers.name = 'Павловский завод' AND categories.name = 'Стеновые и фасадные материалы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'LPDDM4', 'Штукатурка гипсовая', units.id, 500.00, suppliers.id, manufacturers.id, categories.id, 6, 38, 'Штукатурка гипсовая Knauf Ротбанд 30 кг', 'LPDDM4.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Knauf' AND manufacturers.name = 'Knauf' AND categories.name = 'Сухие строительные смеси и гидроизоляция';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'LQ48MW', 'Штукатурка гипсовая', units.id, 462.00, suppliers.id, manufacturers.id, categories.id, 6, 33, 'Штукатурка гипсовая Knauf МП-75 машинная 30 кг', 'LQ48MW.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Weber' AND manufacturers.name = 'Weber' AND categories.name = 'Сухие строительные смеси и гидроизоляция';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'O43COU', 'Шпаклевка', units.id, 750.00, suppliers.id, manufacturers.id, categories.id, 1, 16, 'Шпаклевка полимерная Weber.vetonit LR + для сухих помещений белая 20 кг', 'O43COU.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'ВОЛМА' AND manufacturers.name = 'ВОЛМА' AND categories.name = 'Сухие строительные смеси и гидроизоляция';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'M26EXW', 'Клей для плитки, керамогранита и камня', units.id, 340.00, suppliers.id, manufacturers.id, categories.id, 8, 0, 'Клей для плитки, керамогранита и камня Крепс Усиленный серый (класс С1) 25 кг', 'M26EXW.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Knauf' AND manufacturers.name = 'Knauf' AND categories.name = 'Сухие строительные смеси и гидроизоляция';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'K0YACK', 'Смесь цементно-песчаная', units.id, 160.00, suppliers.id, manufacturers.id, categories.id, 8, 19, 'Смесь цементно-песчаная (ЦПС) 300 по ТУ MixMaster Универсал 25 кг', 'K0YACK.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'MixMaster' AND manufacturers.name = 'MixMaster' AND categories.name = 'Сухие строительные смеси и гидроизоляция';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'ASPXSG', 'Ровнитель', units.id, 711.00, suppliers.id, manufacturers.id, categories.id, 10, 20, 'Ровнитель (наливной пол) финишный Weber.vetonit 4100 самовыравнивающийся высокопрочный 20 кг', 'ASPXSG.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Weber' AND manufacturers.name = 'Weber' AND categories.name = 'Сухие строительные смеси и гидроизоляция';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'ZKQ5FF', 'Лезвие для ножа', units.id, 65.00, suppliers.id, manufacturers.id, categories.id, 6, 6, 'Лезвие для ножа Hesler 18 мм прямое (10 шт.)', 'ZKQ5FF.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Hesler' AND manufacturers.name = 'Hesler' AND categories.name = 'Ручной инструмент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '4WZEOT', 'Лезвие для ножа', units.id, 110.00, suppliers.id, manufacturers.id, categories.id, 6, 17, 'Лезвие для ножа Armero 18 мм прямое (10 шт.)', '4WZEOT.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Armero' AND manufacturers.name = 'Armero' AND categories.name = 'Ручной инструмент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '4JR1HN', 'Шпатель', units.id, 26.00, suppliers.id, manufacturers.id, categories.id, 6, 7, 'Шпатель малярный 100 мм с пластиковой ручкой', '4JR1HN.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Hesler' AND manufacturers.name = 'Hesler' AND categories.name = 'Ручной инструмент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'Z3XFSP', 'Нож строительный', units.id, 63.00, suppliers.id, manufacturers.id, categories.id, 8, 5, 'Нож строительный Hesler 18 мм с ломающимся лезвием пластиковый корпус', 'Z3XFSP.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Hesler' AND manufacturers.name = 'Hesler' AND categories.name = 'Ручной инструмент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'I6MH89', 'Валик', units.id, 326.00, suppliers.id, manufacturers.id, categories.id, 12, 3, 'Валик Wenzo Roma полиакрил 250 мм ворс 18 мм для красок грунтов и антисептиков на водной основе с рукояткой', 'I6MH89.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Wenzo Roma' AND manufacturers.name = 'Wenzo Roma' AND categories.name = 'Ручной инструмент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '83M5ME', 'Кисть', units.id, 122.00, suppliers.id, manufacturers.id, categories.id, 9, 26, 'Кисть плоская смешанная щетина 100х12 мм для красок и антисептиков на водной основе', '83M5ME.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Armero' AND manufacturers.name = 'Armero' AND categories.name = 'Ручной инструмент';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '61PGH3', 'Очки защитные', units.id, 184.00, suppliers.id, manufacturers.id, categories.id, 6, 25, 'Очки защитные Delta Plus KILIMANDJARO (KILIMGRIN) открытые с прозрачными линзами', '61PGH3.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'KILIMGRIN' AND manufacturers.name = 'KILIMGRIN' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'GN6ICZ', 'Каска защитная', units.id, 154.00, suppliers.id, manufacturers.id, categories.id, 15, 8, 'Каска защитная Исток (КАС001О) оранжевая', 'GN6ICZ.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Исток' AND manufacturers.name = 'Исток' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'Z3LO0U', 'Очки защитные', units.id, 228.00, suppliers.id, manufacturers.id, categories.id, 9, 11, 'Очки защитные Delta Plus RUIZ (RUIZ1VI) закрытые с прозрачными линзами', 'Z3LO0U.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'RUIZ' AND manufacturers.name = 'RUIZ' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'QHNOKR', 'Маска защитная', units.id, 251.00, suppliers.id, manufacturers.id, categories.id, 2, 22, 'Маска защитная Исток (ЩИТ001) ударопрочная и термостойкая', 'QHNOKR.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Исток' AND manufacturers.name = 'Исток' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'EQ6RKO', 'Подшлемник', units.id, 36.00, suppliers.id, manufacturers.id, categories.id, 17, 22, 'Подшлемник для каски одноразовый', 'EQ6RKO.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Husqvarna' AND manufacturers.name = 'Husqvarna' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '81F1WG', 'Каска защитная', units.id, 1500.00, suppliers.id, manufacturers.id, categories.id, 2, 13, 'Каска защитная Delta Plus BASEBALL DIAMOND V UP (DIAM5UPBCFLBS) белая', '81F1WG.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Delta' AND manufacturers.name = 'Delta' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT '0YGHZ7', 'Очки защитные', units.id, 700.00, suppliers.id, manufacturers.id, categories.id, 9, 36, 'Очки защитные Husqvarna Clear (5449638-01) открытые с прозрачными линзами', '0YGHZ7.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Husqvarna' AND manufacturers.name = 'Husqvarna' AND categories.name = 'Защита лица, глаз, головы';
INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
SELECT 'O43COU8', 'Товар O43COU8', units.id, 0.00, suppliers.id, manufacturers.id, categories.id, 0, 0, 'Товар добавлен при подготовке импортных данных, так как он встречается в заказах, но отсутствует в исходном файле товаров.', 'O43COU8.png'
FROM units, suppliers, manufacturers, categories
WHERE units.name = 'шт.' AND suppliers.name = 'Не указан' AND manufacturers.name = 'Не указан' AND categories.name = 'Не указано';
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 1, '2025-02-27', '2025-04-20', 1, 'Михайлюк Анна Вячеславовна', 901, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (1, 'PMEZMH', 2);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (1, 'BPV4MM', 2);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 2, '2024-09-28', '2025-04-21', 11, 'Ситдикова Елена Анатольевна', 902, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (2, 'JVL42J', 1);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (2, 'F895RB', 1);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 3, '2025-03-21', '2025-04-22', 2, 'Никифорова Весения Николаевна', 903, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (3, '3XBOTN', 10);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (3, '3L7RCZ', 10);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 4, '2025-02-20', '2025-04-23', 11, 'Сазонов Руслан Германович', 904, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (4, 'S72AM3', 5);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (4, '2G3280', 4);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 5, '2025-03-17', '2025-04-24', 2, 'Михайлюк Анна Вячеславовна', 905, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (5, 'MIO8YV', 2);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (5, 'UER2QD', 2);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 6, '2025-03-01', '2025-04-25', 15, 'Ситдикова Елена Анатольевна', 906, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (6, 'ZR70B4', 1);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (6, 'LPDDM4', 1);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 7, '2025-02-28', '2025-04-26', 3, 'Никифорова Весения Николаевна', 907, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Завершен';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (7, 'LQ48MW', 10);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (7, 'O43COU8', 10);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 8, '2025-03-31', '2025-04-27', 19, 'Сазонов Руслан Германович', 908, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Новый';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (8, 'M26EXW', 5);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (8, 'K0YACK', 4);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 9, '2025-04-02', '2025-04-28', 5, 'Никифорова Весения Николаевна', 909, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Новый';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (9, 'ASPXSG', 5);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (9, 'ZKQ5FF', 1);
INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
SELECT 10, '2025-04-03', '2025-04-29', 19, 'Сазонов Руслан Германович', 910, order_statuses.id
FROM order_statuses WHERE order_statuses.name = 'Новый';
INSERT INTO order_items (order_id, product_article, quantity) VALUES (10, '4WZEOT', 5);
INSERT INTO order_items (order_id, product_article, quantity) VALUES (10, '4JR1HN', 5);
COMMIT;
