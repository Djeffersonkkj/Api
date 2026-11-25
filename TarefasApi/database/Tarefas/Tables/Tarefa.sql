CREATE TABLE "Tarefas"."Tarefa" (
    "Id" SERIAL PRIMARY KEY,
    "Titulo" VARCHAR(100) NOT NULL,
    "Descricao" VARCHAR(500),
    "Concluida" BOOLEAN NOT NULL,
    "DataCriacao" TIMESTAMPTZ NOT NULL
);