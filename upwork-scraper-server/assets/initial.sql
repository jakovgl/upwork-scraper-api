begin;

create table if not exists settings (
    active boolean,
    cookie varchar,
    engagement varchar
);

create table if not exists category (
    id serial primary key,
    name varchar
);

create table if not exists job (
   id bigserial, 
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