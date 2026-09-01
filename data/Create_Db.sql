CREATE DATABASE SistemaDelivery

USE SistemaDelivery

CREATE TABLE Restaurante
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(250) NOT NULL UNIQUE,
    Nipc NVARCHAR(20) NOT NULL UNIQUE,
    Telemovel NVARCHAR(50) NOT NULL,
    Categoria NVARCHAR(50) NOT NULL CHECK (Categoria IN ('FastFood', 'Casual', 'AltaGastronomia', 'Tematicos', 'Regional')),
    Ativo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Cliente
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150) NOT NULL,
    Nif NVARCHAR(20) UNIQUE NOT NULL,
    Telemovel NVARCHAR(50),
    Email NVARCHAR(150) UNIQUE
);

CREATE TABLE Prato
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    RestauranteId INT NOT NULL,
    Nome NVARCHAR(150) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Disponivel BIT NOT NULL DEFAULT 1,

    CONSTRAINT CHK_Preco_NaoNegativo CHECK (Preco >= 0),

    CONSTRAINT FK_Restaurante_Prato
        FOREIGN KEY (RestauranteId)
        REFERENCES Restaurante(Id)
       
);

CREATE TABLE Pedido
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL,
    RestauranteId INT NOT NULL,
    DataPedido DATETIME NOT NULL DEFAULT GETDATE(),
    StatusPedido NVARCHAR(50) NOT NULL DEFAULT 'Preparado' CHECK (StatusPedido IN ('Preparado', 'Cancelado', 'Entregue')),
    Total DECIMAL(10,2) NOT NULL,

    CONSTRAINT CHK_Total_NaoNegativo CHECK (Total >= 0),

    CONSTRAINT FK_Cliente_Pedido
        FOREIGN KEY (ClienteId)
        REFERENCES Cliente(Id),

    CONSTRAINT FK_Restaurante_Pedido
        FOREIGN KEY (RestauranteId)
        REFERENCES Restaurante(Id)
);

CREATE TABLE ItemPedido
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PedidoId INT NOT NULL,
    PratoId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,

    CONSTRAINT CHK_Qtd_Positiva CHECK (Quantidade > 0),
    CONSTRAINT CHK_PU_NaoNegativo CHECK (PrecoUnitario >= 0),

    CONSTRAINT FK_Pedido_ItemPedido
        FOREIGN KEY (PedidoId)
        REFERENCES Pedido(Id),

    CONSTRAINT FK_Prato_ItemPedido
        FOREIGN KEY (PratoId)
        REFERENCES Prato(Id)
);