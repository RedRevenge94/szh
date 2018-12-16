create schema configuration;

create table configuration.notification_settings(
id SERIAL PRIMARY KEY,
name VARCHAR(40) NOT NULL,
value VARCHAR(60),
description VARCHAR(120),
datatype VARCHAR(30) NOT NULL
);

insert into configuration.notification_settings (name, description, datatype)
values 
('SENDER_EMAIL_ADDRESS','Adres email z ktorego wysylane sa powiadomienia email','string'),
('SENDER_EMAIL_PASSWORD','Haslo dla konta email SENDER_EMAIL_ADDRESS','string'),
('SENDER_EMAIL_HOST','Adres serwera SMTP.','string'),
('SENDER_EMAIL_HOST_PORT','Port serwera SMTP','string');