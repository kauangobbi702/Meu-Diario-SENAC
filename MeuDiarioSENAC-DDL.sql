CREATE DATABASE db_diario_senac;
USE db_diario_senac;

CREATE TABLE tb_usuario (
id_usuario INT AUTO_INCREMENT PRIMARY KEY,
nome_usuario VARCHAR(90) NOT NULL,
email_usuario VARCHAR(150) NOT NULL UNIQUE,
senha_usuario VARCHAR(255) NOT NULL);

CREATE TABLE tb_registros (
id_registro INT AUTO_INCREMENT PRIMARY KEY,
id_usuario INT,
titulo VARCHAR(200),
data DATE DEFAULT (CURRENT_DATE),
conteudo VARCHAR(3000),
CONSTRAINT FK_usuario_id FOREIGN KEY (id_usuario) REFERENCES tb_usuario(id_usuario));
