

INSERT INTO TIPOS_RESIDENCIA(TPR_TIPO) VALUES 
('CASA'), 
('APARTAMENTO'), 
('SOBRADO'), 
('KITNET'), 
('LOFT'), 
('STUDIO'), 
('FLAT'), 
('DUPLEX'), 
('TRIPLEX'), 
('COBERTURA'), 
('BANGAL�'), 
('CHAL�'),
('MANS�O'), 
('FAZENDA'),
('S�TIO'), 
('CH�CARA'),
('VILA'),
('CONDOM�NIO'),
('CASA GEMINADA'),
('RESID�NCIA T�RREA');

INSERT INTO TIPOS_USUARIO (TPU_TIPO) VALUES 
('ADMINISTRADOR'),
('CLIENTE');

INSERT INTO TIPOS_TELEFONE (TPT_TIPO) VALUES ('Fixo'), ('Movel');

INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('AEROPORTO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('ALAMEDA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('�REA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('AVENIDA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('CAMPO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('CH�CARA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('COL�NIA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('CONDOM�NIO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('CONJUNTO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('DISTRITO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('ESPLANADA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('ESTA��O');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('ESTRADA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('FAZENDA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('FEIRA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('JARDIM');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('LADEIRA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('LAGO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('LAGOA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('LARGO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('LOTEAMENTO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('N�CLEO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('PARQUE');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('PASSARELA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('P�TIO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('PRA�A');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('QUADRA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('RECANTO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('RESIDENCIAL');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('RODOVIA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('RUA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('SETOR');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('SITIO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('TRAVESSA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('TRECHO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('TREVO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('VALE');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('VEREDA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('VIA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('VIADUTO');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('VIELA');
INSERT INTO TIPOS_LOGRADOURO(TPL_TIPO) VALUES('VILA');

INSERT INTO GENEROS(GNR_GENERO) VALUES 
('MASCULINO'),
('FEMININO'),
('OUTRO');

INSERT INTO BANDEIRAS_CARTAO (BDC_NOME) VALUES
('VISA'),
('MASTERCARD'),
('AMERICAN EXPRESS'),
('DINERS CLUB'),
('DISCOVER'),
('UNIONPAY'),
('JCB'),
('ELO'),
('HIPERCARD');


INSERT INTO GRUPO_PRECIFICACAO (GRP_MARGEM, GRP_PRECIFICACAO) VALUES (1.3, 'BRONZE'), (1.5, 'PRATA'),(1.8, 'OURO');

INSERT INTO TIPOS_PRODUTO (TPP_TIPO) VALUES ('PERIF�RICO'), ('ACESS�RIO'), ('NOTEBOOK'), ('CELULAR');

INSERT INTO STATUS_PEDIDO (STP_STATUS) VALUES 
('EM PROCESSAMENTO'),
('REPROVADO'),
('APROVADO'),
('CANCELADO'),
('EM TRANSPORTE'),
('ENTREGUE'),
('TROCA SOLICITADA'),
('TROCA ACEITA'),
('TROCA CONCLU�DA'),
('TROCA RECUSADA'),
('DEVOLU��O SOLICITADA'),
('DEVOLU��O RECUSADA'),
('DEVOLU��O CONCLU�DA'),
('PEDIDO CONCLUIDO');

INSERT INTO CUPONS (CPN_NOME, CPN_DESCONTO) VALUES('10OFF', 10), ('25OFF', 25), ('50OFF', 50), ('60OFF', 60), ('75OFF', 75);