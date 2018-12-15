alter table measurement.measurement_type
add column name VARCHAR(50);

UPDATE measurement.measurement_type SET name = 'Temperatura' where id = 1;

ALTER TABLE measurement.measurement_type ALTER COLUMN name SET NOT NULL;

alter table measurement.measurement_type
rename column description to acronym;

UPDATE measurement.measurement_type SET acronym = 'temperature' where id = 1;