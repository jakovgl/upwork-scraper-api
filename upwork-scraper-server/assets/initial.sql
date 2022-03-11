begin;

create table if not exists settings (
    active boolean,
    cookie varchar,
    telegram_api_key varchar,
    telegram_chat_id varchar,
    engagement varchar
);

create table if not exists category (
    id serial primary key,
    varchar name
);

create table if not exists job (
   cipher varchar,
   title varchar,
   description varchar,
   amount int,
   currency varchar,
   engagement varchar,
   proposals_tier varchar,
   attributes varchar
);

commit;
end;