CREATE TABLE cultivation.variety(
id SERIAL PRIMARY KEY,
name VARCHAR(25),
plant INT NOT NULL
);

alter table cultivation.variety
add constraint variety_plants_fk foreign key (plant) references cultivation.plants(id);

alter table cultivation.cultivation add column variety int;
alter table cultivation.cultivation 
add constraint cultivation_variety_fk foreign key (variety) references cultivation.variety(id);