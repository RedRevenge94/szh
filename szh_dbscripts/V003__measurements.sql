CREATE SCHEMA measurement;

CREATE TABLE measurement.measurement_type(
id SERIAL PRIMARY KEY,
description VARCHAR(50) NOT NULL,
unit VARCHAR(10) 
);

INSERT INTO measurement.measurement_type (id,description,unit)
values (1,'Temperature','C');

CREATE TABLE measurement.measurements(
id SERIAL PRIMARY KEY,
measurement_type INT NOT NULL,
avr_device INT NOT NULL,
value FLOAT NOT NULL,
date_time timestamp not null
);

alter table measurement.measurements
add constraint avr_measurement_type_fk foreign key (measurement_type) references measurement.measurement_type(id);

alter table measurement.measurements
add constraint avr_avr_device_fk foreign key (avr_device) references devices.avr_device(id);