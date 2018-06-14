using System;
using System.Collections.Generic;

namespace ElevaCase.API.Entities
{
    public static class ElevaContextExtensions
    {
        public static void EnsureSeedDataForContext(this ElevaContext context)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

           
            context.Escolas.RemoveRange(context.Escolas);
            context.SaveChanges();

            // init seed data
            
            var escolas = new List<Escola>()
            {
                new Escola()
                {
                    Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Nome = "Escola Alfa",
                    Turmas = new List<Turma>()
                    {
                        new Turma()
                        {
                            Id = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                            Etapa = "Ensino Médio",
                            Ano = 3,
                            Numero = 3001

                        },
                        new Turma()
                        {
                            Id = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
                            Etapa = "Ensino Médio",
                            Ano = 3,
                            Numero = 3002

                        },
                        new Turma()
                        {
                            Id = new Guid("70a1f9b9-0a37-4c1a-99b1-c7709fc64167"),
                            Etapa = "Ensino Médio",
                            Ano = 2,
                            Numero = 2001
                        },
                        new Turma()
                        {
                            Id = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
                            Etapa = "Ensino Médio",
                            Ano = 2,
                            Numero = 2002
                        },
                        new Turma()
                        {
                            Id = new Guid("a0378e37-a69b-432b-9099-886c5b07078d"),
                            Etapa = "Ensino Médio",
                            Ano = 1,
                            Numero = 1001
                        },
                        new Turma()
                        {
                            Id = new Guid("88c5d6a3-48fd-47a3-ae9d-d55b86f21a2b"),
                            Etapa = "Ensino Médio",
                            Ano = 1,
                            Numero = 1002
                        }
                    }
                },
                new Escola()
                {
                    Id = new Guid("f0905f76-8ea7-41b4-915a-367ae652e367"),
                    Nome = "Escola Omega",
                    Turmas = new List<Turma>()
                    {

                        new Turma()
                        {
                            Id = new Guid("c8f5e2df-b64c-47a0-851b-fc61ab448b56"),
                            Etapa = "Ensino Fundamental",
                            Ano = 9,
                            Numero = 901

                        },
                        new Turma()
                        {
                            Id = new Guid("d6811f8e-eeff-4118-992a-aa42923456c2"),
                            Etapa = "Ensino Fundamental",
                            Ano = 9,
                            Numero = 901

                        },
                        new Turma()
                        {
                            Id = new Guid("9c39f8fd-76d7-49e7-ad35-9f6e03a11754"),
                            Etapa = "Ensino Fundamental",
                            Ano = 8,
                            Numero = 801
                        },
                        new Turma()
                        {
                            Id = new Guid("ce9fb1e1-7d30-4c9d-8fc3-77d359abc0c3"),
                            Etapa = "Ensino Fundamental",
                            Ano = 7,
                            Numero = 701
                        },
                        new Turma()
                        {
                            Id = new Guid("d2c7dad5-9043-4393-9296-0ebe5d358f88"),
                            Etapa = "Ensino Fundamental",
                            Ano = 6,
                            Numero = 601
                        },
                        new Turma()
                        {
                            Id = new Guid("3ccfbacd-ffc7-4ea3-b395-9a1a7e64b34a"),
                            Etapa = "Ensino Fundamental",
                            Ano = 5,
                            Numero = 501
                        },
                        new Turma()
                        {
                            Id = new Guid("a00ca2c7-279b-473f-a69b-1df39c3f4db9"),
                            Etapa = "Ensino Fundamental",
                            Ano = 4,
                            Numero = 401
                        },
                        new Turma()
                        {
                            Id = new Guid("0c2e99fe-af6c-4f8f-8b1e-6e42f727c269"),
                            Etapa = "Ensino Fundamental",
                            Ano = 3,
                            Numero = 301
                        },
                        new Turma()
                        {
                            Id = new Guid("e765e1a2-f921-4bea-89c8-f57dca6419c7"),
                            Etapa = "Ensino Fundamental",
                            Ano = 2,
                            Numero = 201
                        },
                        new Turma()
                        {
                            Id = new Guid("cfcca834-d1cf-4d89-b276-0627f73532d0"),
                            Etapa = "Ensino Fundamental",
                            Ano = 1,
                            Numero = 101
                        }
                    }
                },new Escola()
                {
                    Id = new Guid("ceeb4f88-0d98-4368-b628-ad5594c6f9a0"),
                    Nome = "Escola Zeus",
                    Turmas = new List<Turma>()
                    {
                        new Turma()
                        {
                            Id = new Guid("ee54fa4c-b3d0-4585-9978-5bf729b2424e"),
                            Etapa = "Ensino Médio",
                            Ano = 3,
                            Numero = 3001

                        },
                        new Turma()
                        {
                            Id = new Guid("4164035d-efc0-485a-a0c6-ce82c22b2f44"),
                            Etapa = "Ensino Médio",
                            Ano = 3,
                            Numero = 3002

                        },
                        new Turma()
                        {
                            Id = new Guid("92f9e923-edaa-43b8-90da-fd9ff13044a7"),
                            Etapa = "Ensino Médio",
                            Ano = 2,
                            Numero = 2001
                        },
                        new Turma()
                        {
                            Id = new Guid("dc777436-33cc-4927-ac08-7b35d88c94d9"),
                            Etapa = "Ensino Médio",
                            Ano = 2,
                            Numero = 2002
                        },
                        new Turma()
                        {
                            Id = new Guid("b876edca-fcfd-4daf-8629-14081b638b0f"),
                            Etapa = "Ensino Médio",
                            Ano = 1,
                            Numero = 1001
                        },
                        new Turma()
                        {
                            Id = new Guid("a60a3849-06ec-4f21-bf01-e0b482b611d5"),
                            Etapa = "Ensino Médio",
                            Ano = 1,
                            Numero = 1002
                        },
                        new Turma()
                        {
                            Id = new Guid("d7fb7eeb-7b28-47a2-9286-f11d74c1a894"),
                            Etapa = "Ensino Fundamental",
                            Ano = 9,
                            Numero = 901

                        },
                        new Turma()
                        {
                            Id = new Guid("4f98dfe2-e339-413e-ad9e-4cbf9e9c18f6"),
                            Etapa = "Ensino Fundamental",
                            Ano = 9,
                            Numero = 901

                        },
                        new Turma()
                        {
                            Id = new Guid("9676de45-4f9f-4feb-903b-f3a57f183137"),
                            Etapa = "Ensino Fundamental",
                            Ano = 8,
                            Numero = 801
                        },
                        new Turma()
                        {
                            Id = new Guid("2a3311d0-71be-40c4-9ba9-09f0a3c267b2"),
                            Etapa = "Ensino Fundamental",
                            Ano = 7,
                            Numero = 701
                        },
                        new Turma()
                        {
                            Id = new Guid("5869a0f1-fe42-4197-be03-6e4549db9475"),
                            Etapa = "Ensino Fundamental",
                            Ano = 6,
                            Numero = 601
                        },
                        new Turma()
                        {
                            Id = new Guid("1d3bce4d-65e5-4d18-9094-4259242fc6da"),
                            Etapa = "Ensino Fundamental",
                            Ano = 5,
                            Numero = 501
                        },
                        new Turma()
                        {
                            Id = new Guid("0b6f9bb4-3c83-4c9c-ab66-163b495bbddf"),
                            Etapa = "Ensino Fundamental",
                            Ano = 4,
                            Numero = 401
                        },
                        new Turma()
                        {
                            Id = new Guid("9baeadc9-9df1-4b87-9813-e0b0287fad6e"),
                            Etapa = "Ensino Fundamental",
                            Ano = 3,
                            Numero = 301
                        },
                        new Turma()
                        {
                            Id = new Guid("429662a7-2e94-4854-b49a-66506d037248"),
                            Etapa = "Ensino Fundamental",
                            Ano = 2,
                            Numero = 201
                        },
                        new Turma()
                        {
                            Id = new Guid("866ec7f5-8a00-45fa-b0f5-31e9ebfeceb6"),
                            Etapa = "Ensino Fundamental",
                            Ano = 1,
                            Numero = 101
                        }
                    }
                }
            };

            context.Escolas.AddRange(escolas);
            context.SaveChanges();
        }
    }
}
