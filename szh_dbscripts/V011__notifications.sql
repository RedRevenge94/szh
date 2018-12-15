CREATE TABLE measurement.notifications(
id SERIAL PRIMARY KEY,
tunnel INT NOT NULL,
condition VARCHAR(2) NOT NULL,
measurement_type INT NOT NULL,
value FLOAT NOT NULL,
repeat_after INT,
receivers VARCHAR(2000)
);

alter table measurement.notifications
add constraint tunnel_notifications_type_fk foreign key (tunnel) references cultivation.tunnels(id);

alter table measurement.notifications
add constraint measurement_type_notifications_type_fk foreign key (measurement_type) references measurement.measurement_type(id);