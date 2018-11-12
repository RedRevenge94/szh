CREATE TABLE breeding.plants(
id SERIAL PRIMARY KEY,
name VARCHAR(25)
);

CREATE TABLE breeding.breeding(
id SERIAL PRIMARY KEY,
name VARCHAR(25),
plant INT NOT NULL,
pieces INT,
tunnel INT NOT NULL,
start_date timestamp
);

alter table breeding.breeding
add constraint breeding_plant_fk foreign key (plant) references breeding.plants(id);

alter table breeding.breeding
add constraint breeding_tunnel_fk foreign key (tunnel) references breeding.tunnels(id);