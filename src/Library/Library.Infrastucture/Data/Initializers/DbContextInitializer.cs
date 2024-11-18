﻿using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Auth;
using Library.Domain.Specifications;

namespace Library.Infrastucture.Data.Initializers
{
    public class DbContextInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public DbContextInitializer(IUnitOfWork unitOfWork,
                                    IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task InitializeAsync()
        {
            await InitializeRolesAsync(/*CancellationToken.None*/);

            await InitializeAdminUserAsync(/*CancellationToken.None*/);

            await InitializeGenresAuthorsAndBooksAsync(/*CancellationToken.None*/);

        }


        public async Task InitializeRolesAsync(CancellationToken cancellationToken = default)
        {
            var roleInitializer = new RoleInitializer(_unitOfWork.Roles, _unitOfWork);
            await roleInitializer.InitializeRolesAsync(cancellationToken);
        }

        public async Task InitializeAdminUserAsync(CancellationToken cancellationToken = default)
        {
            var adminUserInitializer = new AdminUserInitializer(_unitOfWork.Users,
                _unitOfWork.Roles,
                _unitOfWork.RefreshTokens,
                _unitOfWork,
                _passwordHasher);

            await adminUserInitializer.InitializeAdminAsync(cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        //public async Task InitializeAdminUser(CancellationToken cancellationToken = default)
        //{
        //    var adminRole = _unitOfWork.Repository<Role>().GetAll()
        //        .FirstOrDefault(r => r.Name.Equals(Roles.Admin.StringValue()));

        //    var user = new User("nika_susch2003", "nikaAdmin@gmail.com", _passwordHasher.HashPassword("Nika2003!"), adminRole);


        //    var userRepository = _unitOfWork.Repository<User>();
        //    var refreshTokenRepository = _unitOfWork.Repository<RefreshToken>();

        //    if (!userRepository.GetAll().ToList().Any())
        //    {
        //        userRepository.Create(user);
        //    }

        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Guid.NewGuid().ToString(),
        //        UserId = user.Id,
        //        ExpiryDate = DateTime.UtcNow.AddDays(7)
        //    };

        //    if (!refreshTokenRepository.GetAll().ToList().Any())
        //    {
        //        refreshTokenRepository.Create(refreshToken);
        //    }

        //    await _unitOfWork.SaveChangesAsync(cancellationToken);
        //}

        public async Task InitializeGenresAuthorsAndBooksAsync(CancellationToken cancellationToken = default)
        {
            var genres = new List<Genre>()
            {
                new Genre("роман"),
                new Genre("повесть"),
                new Genre("антиутопия"),
                new Genre("трилогия")
            };

            var authorList = new List<Author>()
            {
                new Author("Кадзуо", "Исигуро", new DateTime(1954, 11, 8), "Япония"),
                new Author("Лев", "Толстой", new DateTime(1828, 9, 9), "Россия"),
                new Author("Стивен", "Кинг", new DateTime(1947, 9, 21), "США"),
                new Author("Рэй", "Брэдбери", new DateTime(1920, 8, 22), "США"),
                new Author("Михаил", "Булгаков", new DateTime(1891, 3, 15), "Россия")
            };

            var books = new List<Book>()
            {
                new Book(
                    name: "Не отпускай меня",
                    iSBN: "978-5-699-37388-8",
                    genre: genres[2],
                    description: "\"Не отпускай меня\" – пронзительная книга, которая по праву входит в список 100 лучших английских романов всех времен по версии журнала \"Time\". Ее автор урожденный японец, выпускник литературного семинара Малькольма Брэдбери и лауреат Буксровской премии (за роман \"Остаток дня\").\r\n\r\nТридцатилетняя Кэти вспоминает свое детство в привилегированной школе Хейлшем, полное странных недомолвок, половинчатых откровений и подспудной угрозы.\r\n\r\nЭто роман-притча. Это история любви, дружбы и памяти. Это предельное овеществление метафоры \"служить всей жизнью\".",
                    count: 2,
                    authors: [authorList[0]],
                    imageURL: "https://lh3.googleusercontent.com/pw/AP1GczOdleUSoYYUAtKaFBcWa1NESdn3cY7PlNPho0zES8vG0DxQM87KyXTx6tJbrPcEV1W1K0rV9nNSSSMttXSmJCgw1lTUWvZeX4Ocfmtr7u6XjBtonLJ4WEqZxyXdm1eWdUv-ha7i6QiSyQTx-2fNcsjj=w507-h895-s-no-gm?authuser=0"
                    ),

                new Book(
                    name: "Художник зыбкого мира",
                    iSBN: "978-5-04-105023-8",
                    genre: genres[0],
                    description: "\"Художник зыбкого мира\" – второй роман нобелевского лауреата по литературе и обладателя Букеровской премии. Герой этой книги – один из самых знаменитых живописцев довоенной Японии, тихо доживающий свои дни и мечтающий лишь удачно выдать замуж дочку. Но в воспоминаниях он по-прежнему там – в веселых кварталах старого Токио, в зыбком, сумеречном мире приглушенных страстей, дискуссий о красоте и потаенных удовольствий.",
                    count: 1,
                    authors: [authorList[0]]),

                new Book(
                    name: "Когда мы были сиротами",
                    iSBN: "978-5-04-109858-2",
                    genre: genres[0],
                    description: "От урожденного японца, выпускника литературного семинара Малькольма Брэдбери, лауреата Букеровской премии за \"Остаток дня\", – изысканный роман, в котором парадоксально сочетаются традиции \"черного детектива\" 1930-х годов и \"культурологической прозы\" конца XX – начала XXI века.\r\n\r\nИзвестнейший детектив-интеллектуал Кристофер Бэнкс с детства мечтает раскрыть тайну исчезновения своих родителей – и наконец ему представляется возможность сделать это, в очень неспокойное время отправившись по маршруту Лондон – Шанхай. Однако расследование Кристофера и его экзотическое путешествие постепенно превращаются в странствие из Настоящего в Прошлое, из мира иллюзий – в мир жестокой реальности...",
                    count: 3,
                    authors: [authorList[0]],
                    imageURL: "https://lh3.googleusercontent.com/pw/AP1GczMe4ze3u62wgRyOUN62O3LOxQzC3f8CzvdzJsRxK8--wtXPIv-LLZKBAY6Kpkx8mco7HEGpCcPYG6kejmqZcEbuUxj3v54U0CVHJyCaQNKzi6jdQMTOPWn0QEsqAaEUBNi91AvA3X4q_yVAKkkW-0hZ=w395-h620-s-no-gm?authuser=0"
                    ),

                new Book(
                    name: "Погребенный великан",
                    iSBN: "978-5-04-163953-2",
                    genre: genres[0],
                    description: "\"Погребенный великан\" – непревзойденная история о любви и одиночестве, о войне и мести, о времени и памяти.\r\n\r\nИсигуро переносит нас в средневековую Англию, где люди еще помнят короля Артура, где живы легенды, а зеленые холмы объяты туманом. Немолодая пара, Аксель и Беатрис, исполненные желания отыскать сына, которого не видели уже долгие годы, отправляются в путь, прочь от родной деревни. Дороги опутаны неведомой хмарью, заставляющей забыть только что прожитый час. Время и пространство схлопываются в этом захватывающем путешествии жизни. Куда же оно направит их? С кем неожиданно сведет?",
                    count: 1,
                    authors: [authorList[0]],
                    imageURL: "https://lh3.googleusercontent.com/pw/AP1GczMVL8XoVG4cPEHz9nsh4G99mMwtcTs5Pegwu5AuAAD35frWMfJUN_2lpq1CAwm09xdmWObFsJYmv17tRmQ84XylUSdY8tjFFmu1_2cgLZa0wfyTeYlgEswp9IVMSV9qL-DriZ7vclULO8XrL85FogTB=w394-h620-s-no-gm?authuser=0"
                    ),

                new Book(
                    name: "Детство",
                    iSBN: "978-5-04-121706-8",
                    genre: genres[1],
                    description: "Нежная и лиричная, наполненная любовью и милосердием автобиографическая повесть Льва Николаевича Толстого \"Детство\" – одна из самых востребованных книг о лучшей поре жизни. Она написана от первого лица так искренне и психологически безупречно, что у читателей возникает чувство дружеского разговора с главным героем, десятилетним мальчиком Николенькой Иртеньевым. В этом разговоре мы узнаём свои переживания: первые радости, первую любовь, первые горькие утраты; учимся верить, сострадать и понимать тех, кто нас окружает.",
                    count: 5,
                    authors: [authorList[1]]),

                new Book(
                    name: "Детство. Отрочество. Юность",
                    iSBN: "978-5-389-21867-3",
                    genre: genres[3],
                    description: "Повесть \"Детство\", впервые опубликованная в 1852 году в журнале \"Современник\", стала литературным дебютом Л. Н. Толстого, выдающегося русского писателя и мыслителя. Опираясь на детские воспоминания и дневниковые записи, Толстой, однако, писал не автобиографию и не мемуары. Автор пытался раскрыть универсальные законы развития человеческой души, её загадочную изменчивую \"диалектику\", \"высказать такие тайны, которые нельзя сказать простым словом\", что как раз и составляет главную цель искусства, как считал автор. Впоследствии повесть \"Детство\" вместе с \"Отрочеством\" (1852-1854) и \"Юностью\" (1855-1857) соединились в трилогию и стали \"не только копилкой будущих литературных замыслов, но и некой абсолютной точкой на пути, раз и навсегда открытым континентом на карте толстовского мира\" (И. Н. Сухих).",
                    count: 0,
                    authors: [authorList[1]]),

                new Book(
                    name: "Анна Каренина",
                    iSBN: "978-5-389-04935-2",
                    genre: genres[0],
                    description: "\"Анна Каренина\" – лучший роман о женщине, написанный в XIX веке. По словам Ф. М. Достоевского, \"Анна Каренина\" поразила современников \"не только вседневностью содержания, но и огромной психологической разработкой души человеческой, страшной глубиной и силой\". Уже к началу 1900-х годов роман Толстого был переведен на многие языки мира, а в настоящее время входит в золотой фонд мировой литературы.",
                    count: 3,
                    authors: [authorList[1]]),

                new Book(
                    name: "Оно",
                    iSBN: "978-5-17-065495-6",
                    genre: genres[0],
                    description: "В маленьком провинциальном городке Дерри много лет назад семерым подросткам пришлось столкнуться с кромешным ужасом – живым воплощением ада. Прошли годы... Подростки повзрослели, и ничто, казалось, не предвещало новой беды. Но кошмар прошлого вернулся, неведомая сила повлекла семерых друзей назад, в новую битву со Злом. Ибо в Дерри опять льется кровь и бесследно исчезают люди. Ибо вернулось порождение ночного кошмара, настолько невероятное, что даже не имеет имени...",
                    count: 3,
                    authors: [authorList[2]]),

                new Book(
                    name: "Под куполом",
                    iSBN: "978-5-17-074852-5",
                    genre: genres[0],
                    description: "История маленького городка, который настигла большая беда.\r\n\r\nОднажды его, вместе со всеми обитателями, накрыло таинственным невидимым куполом, не позволяющим ни покинуть город, ни попасть туда извне.\r\n\r\nЧто теперь будет в городке? Что произойдет с его жителями?\r\n\r\nВедь когда над человеком не довлеет ни закон, ни страх наказания – слишком тонкая грань отделяет его от превращения в жестокого зверя. Кто переступит эту грань, а кто – нет?",
                    count: 2,
                    authors: [authorList[2]]),

                new Book(
                    name: "Сияние",
                    iSBN: "978-5-17-084078-6",
                    genre: genres[0],
                    description: "Культовый роман Стивена Кинга. Роман, который и сейчас, спустя тридцать с лишним лет после триумфального выхода в свет, читается так, словно был написан вчера. Книга, вновь и вновь издающаяся едва ли не на всех языках мира. Перед вами – одно из лучших произведений Мастера в новом переводе!\r\n\r\n...Проходят годы, десятилетия, но потрясающая история писателя Джека Торранса, его сынишки Дэнни, наделенного необычным даром, и поединка с темными силами, обитающими в роскошном отеле \"Оверлук\", по-прежнему завораживает и держит в неослабевающем напряжении читателей самого разного возраста... Какое же бесстрашное воображение должно быть у писателя, создавшего подобную книгу!",
                    count: 4,
                    authors: [authorList[2]]),

                new Book(
                    name: "451 градус по Фаренгейту",
                    iSBN: "978-5-699-92359-5",
                    genre: genres[2],
                    description: "Пожарные, которые разжигают пожары, книги, которые запрещено читать, и люди, которые уже почти перестали быть людьми... Роман Рэя Брэдбери \"451 по Фаренгейту\" – это классика научной фантастики, ставшая классикой мирового кинематографа в воплощении знаменитого французского режиссера Франсуа Трюффо. А на очереди – экранизация этой книги прекрасным актером и режиссером Мелом Гибсоном.",
                    count: 1,
                    authors: [authorList[3]]),

                new Book(
                    name: "Вино из одуванчиков",
                    iSBN: "978-5-699-55169-9",
                    genre: genres[0],
                    description: "Войдите в светлый мир двенадцатилетнего мальчика и проживите вместе с ним одно лето, наполненное событиями радостными и печальными, загадочными и тревожными; лето, когда каждый день совершаются удивительные открытия, главное из которых – ты живой, ты дышишь, ты чувствуешь!\r\n\r\n\"Вино из одуванчиков\" Рэя Брэдбери – классическое произведение, вошедшее в золотой фонд мировой литературы. Это одна из тех книг, которые хочется перечитывать вновь и вновь.",
                    count: 2,
                    authors: [authorList[3]]),

                new Book(
                    name: "Мастер и Маргарита",
                    iSBN: "978-5-389-01686-6",
                    genre: genres[0],
                    description: "«Мастер и Маргарита» Михаила Булгакова – самое удивительное и загадочное произведение ХХ века.\r\n\r\nОпубликованный в середине 1960-х, этот роман поразил читателей необычностью замысла, красочностью и фантастичностью действия, объединяющего героев разных эпох и культур. Автор создал «роман в романе», где сплетены воедино религиозно-историческая мистерия, восходящая к легенде о распятом Христе, московская «буффонада» и сверхъестественные сцены с персонажами, воплощающими некую темную силу, которая однако «вечно хочет зла и вечно совершает благо».\r\n\r\n«Есть в этой книге какая-то безрасчетность, какая-то предсмертная ослепительность большого таланта...» – писал Константин Симонов в своем предисловии к первой публикации романа, открывшей всему миру большого художника, подлинного Мастера слова.",
                    count: 5,
                    authors: [authorList[4]]),

                new Book(
                    name: "Собачье сердце",
                    iSBN: "978-5-17-115274-1",
                    genre: genres[1],
                    description: "\"Собачье сердце\", гениальная повесть Михаила Булгакова, написанная еще в 1925 году, едва не стоившая автору свободы и до 1987 года издававшаяся лишь за рубежом и ходившая по рукам в самиздате, в представлениях не нуждается.\r\n\r\nЧуть ли не до последней буквы разобранная на цитаты история милого пса Шарика, превращенного, благодаря эксперименту профессора Преображенского, в типичного \"красного хама\" Полиграфа Шарикова, среди русскоязычных читателей вот уже нескольких поколений носит поистине культовый статус.",
                    count: 1,
                    authors: [authorList[4]])
            };


            authorList[0].Books.Add(books[0]);
            authorList[0].Books.Add(books[1]);
            authorList[0].Books.Add(books[2]);
            authorList[0].Books.Add(books[3]);

            authorList[1].Books.Add(books[4]);
            authorList[1].Books.Add(books[5]);
            authorList[1].Books.Add(books[6]);

            authorList[2].Books.Add(books[7]);
            authorList[2].Books.Add(books[8]);
            authorList[2].Books.Add(books[9]);

            authorList[3].Books.Add(books[10]);
            authorList[3].Books.Add(books[11]);

            authorList[4].Books.Add(books[12]);
            authorList[4].Books.Add(books[13]);


            var bookRepository = _unitOfWork.Books;
            var authorRepository = _unitOfWork.Authors;
            var genreRepository = _unitOfWork.Genres;

            if (!(await bookRepository.ListAsync(new EmptySpecification<Book>(), cancellationToken)).Any() &&
                !(await authorRepository.ListAsync(new EmptySpecification<Author>(), cancellationToken)).Any() &&
                !(await genreRepository.ListAsync(new EmptySpecification<Genre>(), cancellationToken)).Any())
            {
                foreach (var genre in genres)
                {
                    genreRepository.Create(genre);
                }

                for (int i = 0; i < books.Count; i++)
                {
                    bookRepository.Create(books[i]);

                    for (int j = 0; j < authorList.Count; j++)
                    {
                        authorRepository.Create(authorList[j]);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
