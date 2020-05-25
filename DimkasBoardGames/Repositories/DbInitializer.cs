using System;
using System.Collections.Generic;
using System.Linq;
using DimkasBoardGames.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DimkasBoardGames.Repositories
{
    public class DbInitializer
    {
        private static Dictionary<string, BoardGameGenre> boardGameGenres;


        public void Seed(IServiceProvider provider)
        {
            using (var serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                InsertData(context);
            }
        }

        public static void InsertData(AppDbContext context)
        {
            if (!context.BoardGameGenres.Any())
            {
                context.BoardGameGenres.AddRange(BoardGameGenres.Select(pair => pair.Value));
            }

            if (!context.BoardGames.Any())
            {
                context.AddRange
                (
                    new BoardGame
                    {
                        Title = "Пандемия",
                        ShortDescription =
                            "Кооперативная семейная игра, в которой игроки пытаются спасти человечество от биологического апокалипсиса.",
                        Price = 990.00M,
                        Image = GetImages()[0],
                        LongDescription =
                            "По миру распространяются смертельные болезни. То тут, то там происходят вспышки и начинаются эпидемии. Спасти человечество призвана команда специалистов, путешествующая по самым отдаленным уголкам планеты. Им предстоит работать сообща, гасить очаги болезней и всеми возможными способами пытаться изобрести лекарства. Но хватит ли им времени? Вступайте в игру и от Вашей команды будет зависеть ответ на этот вопрос.",
                        Feedbacks = new List<Feedback>
                        {
                            new Feedback
                            {
                                Message = "New Message",
                                UserName = "User"
                            },
                            new Feedback
                            {
                                Message = "Second Message",
                                UserName = "Samuel"
                            }
                        },
                        BoardGameGenre = BoardGameGenres["Cooperative"]
                    },
                    new BoardGame
                    {
                        Title = "О Мышах и Тайнах",
                        ShortDescription =
                            "Кооперативная семейная игра. Игроки берут на себя роль мышек, которые спасают королевство от злой ведьмы.",
                        Price = 1500.00M,
                        Image = GetImages()[1],
                        LongDescription =
                            "О мышах и тайнах – замечательная и очень атмосферная кооперативная игра. Участники выступают в роли отважных мышей, некогда бывших людьми. Злая колдунья пытается захватить власть в королевстве, а задача наших героев – всеми силами ей помешать. На самом деле игра очень простая и подойдет даже для детей младшего школьного возраста. Разработчики позаботились об отличном и длинном сюжете и великолепных художественных вставках между миссиями. Искусственный интеллект, в свою очередь, будет не только подбрасывать новые угрозы участникам, но и реагировать на каждое их действие. Иногда придется хорошо подумать, прежде чем принять решение, в других же ситуациях выход будет очевиден. Если подбить итог, то перед нами замечательная интерактивная сказка, в которой игрокам достается роль главных героев.",
                        BoardGameGenre = BoardGameGenres["Strategy"]
                    },
                    new BoardGame
                    {
                        Title = "Цивилизация",
                        ShortDescription =
                            "Стратегическая игра. Игроки становятся во главе одной из мировых цивилизаций и стараются развить ее до победы.",
                        Price = 1900.00M,
                        Image = GetImages()[2],
                        LongDescription =
                            "В настольной игре \"Цивилизация Сида Мейера\" от 2-х до 4-х игроков берут на себя роли великих лидеров исторически реальных цивилизаций, каждая из которых обладает своими уникальными способностями. Игроки могут исследовать модульную карту игры, строить города и здания, сражаться, исследовать всё более совершенные технологии и привлекать великие умы своим уровнем культуры. Неважно, какой стиль игры вы предпочтёте, главное привести свой народ к процветанию!",
                        BoardGameGenre = BoardGameGenres["Strategy"]
                    },
                    new BoardGame
                    {
                        Title = "Каркасон",
                        ShortDescription =
                            "Это одна из самых популярных настольных стратегий во всем мире.",
                        Price = 400.00M,
                        Image = GetImages()[3],
                        LongDescription =
                            "Оплетенный душистыми виноградниками с вершины холма смотрит на нас бойницами неприступных башен величественный замок Каркассон. Пережив смуту средних веков и десятки ожесточенных осад, дошел он до наших дней, чтобы радовать глаз праздных соглядатаев, посещающих одноименный город на юге Франции. Так и стоял он, неприступный и прекрасный, напоминая потомкам о днях минувших... пока однажды непростой немецкий турист по имени Клаус, взглянув на остроконечные шпили этого архитектурного ансамбля, не изменил навсегда судьбу его названия, вписав новую строку в многовековую историю Каркассона.",
                        BoardGameGenre = BoardGameGenres["Strategy"]
                    },
                    new BoardGame
                    {
                        Title = "Ужас Аркхэма ЖКИ",
                        ShortDescription =
                            "Карточный Ужас Аркхэма - это сюжетная кооперативная игра с элементами legacy.",
                        Price = 800.00M,
                        Image = GetImages()[3],
                        LongDescription =
                            "Загадочные события, происходящие в Аркхэме, вновь манят сыщиков и предлагают им поучаствовать в захватывающих приключениях. Мистические происшествия, тайные знаки, жуткие чудовища и гнетущая атмосфера опасности, непременно знакомые Вам, если Вы знаете об Аркхэме не по наслышке, все это вновь ожидает Вас. Присоединяйтесь к команде, либо рискните пойти на расследование в одиночку, в любом случае Вам предстоит окунуться в одно из самых увлекательных приключений в Вашей жизни.",
                        BoardGameGenre = BoardGameGenres["Cooperative"]
                    },
                    new BoardGame
                    {
                        Title = "Кодовые Имена, Дуэт",
                        ShortDescription = "Это кооперативная игра, сделанная специально для двоих.",
                        Price = 500.00M,
                        Image = GetImages()[4],
                        LongDescription =
                            "Хоть у шпионов должны быть железные нервы, они все же остаются людьми. Устав от изматывающего противостояния, ведущие секретные разведки решили провести совместную операцию, чтобы справиться с противником мирового масштаба. Встречайте продолжение легендарных Кодовых Имен – Codenames Duet!",
                        BoardGameGenre = BoardGameGenres["Cooperative"]
                    }
                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, BoardGameGenre> BoardGameGenres
        {
            get
            {
                if (boardGameGenres == null)
                {
                    var genresList = new BoardGameGenre[]
                    {
                        new BoardGameGenre() {GenreName = "Cooperative"},
                        new BoardGameGenre() {GenreName = "Strategy"},
                    };

                    boardGameGenres = new Dictionary<string, BoardGameGenre>();

                    foreach (var genre in genresList)
                    {
                        boardGameGenres.Add(genre.GenreName, genre);
                    }
                }

                return boardGameGenres;
            }
        }


        private static List<Image> GetImages()
        {
            return new List<Image>
            {
                new Image {Name = "Pandemic.jpg"},
                new Image {Name = "MiceAndMystics.jpg"},
                new Image {Name = "Civilization.jpg"},
                new Image {Name = "Carcasone.jpg"},
                new Image {Name = "ArkhamHorror.jpg"},
                new Image {Name = "CodenamesDuet.jpg"},
            };
        }
    }
}