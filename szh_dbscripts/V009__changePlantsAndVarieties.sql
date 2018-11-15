ALTER TABLE cultivation.cultivation DROP CONSTRAINT  cultivation_variety_fk;
ALTER TABLE cultivation.variety DROP CONSTRAINT  variety_plants_fk;
ALTER TABLE cultivation.plants DROP COLUMN name;
ALTER TABLE cultivation.variety DROP COLUMN plant;
ALTER TABLE cultivation.plants ADD COLUMN plant_species INT NOT NULL;
ALTER TABLE cultivation.plants ADD COLUMN plant_variety INT;

CREATE TABLE cultivation.plant_species(
id SERIAL PRIMARY KEY,
name VARCHAR(25)
);

alter table cultivation.plants
add constraint plants_plantsSpecies_fk foreign key (plant_species) references cultivation.plant_species(id);

alter table cultivation.plants
add constraint plants_variety_fk foreign key (plant_variety) references cultivation.variety(id);
