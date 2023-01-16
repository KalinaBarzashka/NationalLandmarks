-- 01. Seed Area data
INSERT INTO [NationalLandmarks.Server].[dbo].[Areas] ([Name], CreatedOn, CreatedByUsername, IsDeleted)
VALUES (N'Благоевград', GETUTCDATE(), 'SYSTEM', 0),
(N'Бургас', GETUTCDATE(), 'SYSTEM', 0),
(N'Варна', GETUTCDATE(), 'SYSTEM', 0),
(N'Велико Търново', GETUTCDATE(), 'SYSTEM', 0),
(N'Видин', GETUTCDATE(), 'SYSTEM', 0),
(N'Враца', GETUTCDATE(), 'SYSTEM', 0),
(N'Габрово', GETUTCDATE(), 'SYSTEM', 0),
(N'Добрич', GETUTCDATE(), 'SYSTEM', 0),
(N'Кърджали', GETUTCDATE(), 'SYSTEM', 0),
(N'Кюстендил', GETUTCDATE(), 'SYSTEM', 0),
(N'Ловеч', GETUTCDATE(), 'SYSTEM', 0),
(N'Монтана', GETUTCDATE(), 'SYSTEM', 0),
(N'Пазарджик', GETUTCDATE(), 'SYSTEM', 0),
(N'Перник', GETUTCDATE(), 'SYSTEM', 0),
(N'Плевен', GETUTCDATE(), 'SYSTEM', 0),
(N'Пловдив', GETUTCDATE(), 'SYSTEM', 0),
(N'Разград', GETUTCDATE(), 'SYSTEM', 0),
(N'Русе', GETUTCDATE(), 'SYSTEM', 0),
(N'Силистра', GETUTCDATE(), 'SYSTEM', 0),
(N'Сливен', GETUTCDATE(), 'SYSTEM', 0),
(N'Смолян', GETUTCDATE(), 'SYSTEM', 0),
(N'София', GETUTCDATE(), 'SYSTEM', 0),
(N'София-град', GETUTCDATE(), 'SYSTEM', 0),
(N'Стара Загора', GETUTCDATE(), 'SYSTEM', 0),
(N'Търговище', GETUTCDATE(), 'SYSTEM', 0),
(N'Хасково', GETUTCDATE(), 'SYSTEM', 0),
(N'Шумен', GETUTCDATE(), 'SYSTEM', 0),
(N'Ямбол', GETUTCDATE(), 'SYSTEM', 0),
(N'Планина', GETUTCDATE(), 'SYSTEM', 0)
GO

-- 02. Get Area Id's
SELECT Id, Name
FROM [NationalLandmarks.Server].[dbo].[Areas]
GO

-- 03. Seed Place data
INSERT INTO [NationalLandmarks.Server].[dbo].[Places] ([Name], AreaId, CreatedOn, CreatedByUsername, IsDeleted)
SELECT N'гр. Банско', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'гр. Благоевград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'гр. Гоце Делчев', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'гр. Мелник', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'гр. Петрич', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'гр. Сандански', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'с. Добърско', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'с. Рожен', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Благоевград'
UNION ALL  
SELECT N'гр. Айтос', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL  
SELECT N'гр. Бургас', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL  
SELECT N'гр. Малко Търново', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL  
SELECT N'гр. Несебър', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL  
SELECT N'гр. Поморие', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL  
SELECT N'гр. Созопол', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL  
SELECT N'гр. Царево', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Бургас'
UNION ALL
SELECT N'гр. Варна', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Варна'
UNION ALL
SELECT N'гр. Девня', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Варна'
UNION ALL
SELECT N'гр. Велико Търново', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Велико Търново'
UNION ALL  
SELECT N'гр. Елена', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Велико Търново'
UNION ALL  
SELECT N'гр. Свищов', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Велико Търново'
UNION ALL  
SELECT N'с. Никюп', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Велико Търново'
UNION ALL
SELECT N'гр. Белоградчик', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Видин'
UNION ALL
SELECT N'гр. Видин', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Видин'
UNION ALL
SELECT N'с. Рабиша', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Видин'
UNION ALL
SELECT N'гр. Враца', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Враца'
UNION ALL
SELECT N'гр. Козлодуй', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Враца'
UNION ALL
SELECT N'гр. Мездра', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Враца'
UNION ALL
SELECT N'с. Челопек', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Враца'
UNION ALL
SELECT N'гр. Габрово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Габрово'
UNION ALL
SELECT N'гр. Дряново', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Габрово'
UNION ALL
SELECT N'гр. Трявна', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Габрово'
UNION ALL
SELECT N'с. Боженците', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Габрово'
UNION ALL
SELECT N'гр. Балчик', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Добрич'
UNION ALL
SELECT N'гр. Добрич', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Добрич'
UNION ALL
SELECT N'гр. Каварна', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Добрич'
UNION ALL
SELECT N'гр. Кърджали', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Кърджали'
UNION ALL
SELECT N'гр. Кюстендил', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Кюстендил'
UNION ALL
SELECT N'с. Стоб', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Кюстендил'
UNION ALL
SELECT N'гр. Ловеч', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'гр. Тетевен', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'гр. Троян', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'с. Брестница', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'с. Деветаки', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'с. Карлуково', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'с. Крушуна', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'с. Къкрина', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'с. Черни Осъм', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ловеч'
UNION ALL
SELECT N'гр. Берковица', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Монтана'
UNION ALL
SELECT N'гр. Монтана', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Монтана'
UNION ALL
SELECT N'гр. Чипровци', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Монтана'
UNION ALL
SELECT N'гр. Батак', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'гр. Брацигово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'гр. Велинград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'гр. Пазарджик', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'гр. Панагюрище', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'гр. Пещера', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'с. Славовица', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'с. Фотиново', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пазарджик'
UNION ALL
SELECT N'гр. Перник', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Перник'
UNION ALL
SELECT N'гр. Трън', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Перник'
UNION ALL
SELECT N'гр. Плевен', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Плевен'

UNION ALL
SELECT N'гр. Асеновград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Калофер', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Карлово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Клисура', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Перущица', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Пловдив', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Сопот', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Хисаря', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'с. Бачково', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'с. Свежен', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'с. Старосел', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Пловдив'
UNION ALL
SELECT N'гр. Исперих', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Разград'
UNION ALL
SELECT N'гр. Разград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Разград'

UNION ALL
SELECT N'гр. Бяла', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Русе'
UNION ALL
SELECT N'гр. Русе', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Русе'
UNION ALL
SELECT N'с. Басарбово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Русе'
UNION ALL
SELECT N'с. Иваново', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Русе'
UNION ALL
SELECT N'с. Каран Върбовка', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Русе'
UNION ALL
SELECT N'гр. Силистра', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Силистра'
UNION ALL
SELECT N'гр. Тутракан', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Силистра'
UNION ALL
SELECT N'с. Сребърна', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Силистра'
UNION ALL
SELECT N'гр. Котел', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Сливен'
UNION ALL
SELECT N'гр. Нова Загора', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Сливен'
UNION ALL
SELECT N'гр. Сливен', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Сливен'
UNION ALL
SELECT N'с. Жеравна', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Сливен'
UNION ALL
SELECT N'гр. Златоград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Смолян'
UNION ALL
SELECT N'гр. Мадан', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Смолян'
UNION ALL
SELECT N'гр. Смолян', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Смолян'
UNION ALL
SELECT N'с. Могилица', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Смолян'
UNION ALL
SELECT N'с. Момчиловци', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Смолян'
UNION ALL
SELECT N'с. Широка лъка', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Смолян'
UNION ALL
SELECT N'гр. Ботевград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'гр. Етрополе', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'гр. Копривщица', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'гр. Самоков', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'с. Алдомировци', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'с. Байлово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'с. Белчин', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'с. Лопян', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'с. Осеновлаг', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'с. Скравена', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София'
UNION ALL
SELECT N'гр. София', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'София-град'
UNION ALL
SELECT N'гр. Казанлък', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Стара Загора'
UNION ALL
SELECT N'гр. Стара Загора', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Стара Загора'
UNION ALL
SELECT N'гр. Чирпан', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Стара Загора'
UNION ALL
SELECT N'гр. Шипка', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Стара Загора'
UNION ALL
SELECT N'гр. Търговище', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Търговище'
UNION ALL
SELECT N'гр. Димитровград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Хасково'
UNION ALL
SELECT N'гр. Ивайловград', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Хасково'
UNION ALL
SELECT N'гр. Маджарово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Хасково'
UNION ALL
SELECT N'гр. Хасково', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Хасково'
UNION ALL
SELECT N'с. Мезек', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Хасково'
UNION ALL
SELECT N'гр. Велики Преслав', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Шумен'
UNION ALL
SELECT N'гр. Плиска', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Шумен'
UNION ALL
SELECT N'гр. Шумен', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Шумен'
UNION ALL
SELECT N'с. Мадара', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Шумен'
UNION ALL
SELECT N'гр. Елхово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ямбол'
UNION ALL
SELECT N'гр. Ямбол', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Ямбол'
UNION ALL
SELECT N'пл. Витоша', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. к-т Пампорово', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Осоговска планина', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Пирин', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Рила', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Рила, Рилски манастир', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Родопи', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Средна гора, проход „Траянови врата“', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Стара планина', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
UNION ALL
SELECT N'пл. Стара планина, Връх Шипка', a.Id, GETUTCDATE(), 'SYSTEM', 0
FROM [NationalLandmarks.Server].[dbo].[Areas] a
WHERE a.[Name] = N'Планина'
GO

-- 04. Seed Landmark data
--INSERT INTO [NationalLandmarks.Server].[dbo].[Landmarks]
--([Name], RegistrationNumber, IsNationalLandmark, [Description], PlaceId, [Address], Latitude, Longitude, ImagePath, UserId, CreatedOn, CreatedByUsername, IsDeleted)
--SELECT N'гр. Банско', a.Id, GETUTCDATE(), 'SYSTEM', 0
--FROM [NationalLandmarks.Server].[dbo].[Areas] a
--WHERE a.[Name] = N'Благоевград'
--UNION ALL  
--SELECT N'гр. Благоевград', a.Id, GETUTCDATE(), 'SYSTEM', 0
--FROM [NationalLandmarks.Server].[dbo].[Areas] a
--WHERE a.[Name] = N'Благоевград'