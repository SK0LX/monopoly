create table if not exists users
(
    id uuid primary key,
    username varchar(64) not null unique,
    password_hash varchar(512) not null,
    created_at_utc timestamp with time zone not null
);

create table if not exists auth_sessions
(
    id uuid primary key,
    user_id uuid not null references users(id) on delete cascade,
    access_token_hash varchar(128) not null unique,
    created_at_utc timestamp with time zone not null,
    expires_at_utc timestamp with time zone not null,
    revoked_at_utc timestamp with time zone null
);

create table if not exists board_cards
(
    id uuid primary key,
    position integer not null unique,
    name varchar(128) not null,
    type varchar(32) not null,
    color_group varchar(64) null,
    is_purchasable boolean not null,
    price integer not null,
    mortgage_price integer not null,
    mortgage_buyout_price integer not null,
    house_price integer not null,
    rent_without_houses integer not null,
    rent_with_one_house integer not null,
    rent_with_two_houses integer not null,
    rent_with_three_houses integer not null,
    rent_with_four_houses integer not null,
    rent_with_hotel integer not null
);
