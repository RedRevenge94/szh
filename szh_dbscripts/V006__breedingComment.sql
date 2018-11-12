CREATE TABLE breeding.breeding_comments(
id SERIAL PRIMARY KEY,
text VARCHAR(255),
breeding INT NOT NULL,
create_date timestamp
);

alter table breeding.breeding_comments
add constraint breedingComment_breeding_fk foreign key (breeding) references breeding.breeding(id);