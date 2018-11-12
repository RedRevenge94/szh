CREATE SCHEMA devices;

CREATE TABLE devices.avr_device(
id SERIAL PRIMARY KEY,
ip VARCHAR(25) NOT NULL,
tunnel INT NOT NULL,
last_update timestamp
);

alter table devices.avr_device
add constraint avr_tunnel_fk foreign key (tunnel) references breeding.tunnels(id);