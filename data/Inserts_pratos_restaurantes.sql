USE SistemaDelivery


-- Restaurantes (um por categoria)
INSERT INTO Restaurante (Nome, Nipc, Telemovel, Categoria, Ativo) VALUES
('Burger Express', '500111222', '910111222', 'FastFood', 1),
('Tasca do Zé', '500333444', '910333444', 'Casual', 1),
('Le Gourmet', '500555666', '910555666', 'AltaGastronomia', 1),
('Planeta Pizza', '500777888', '910777888', 'Tematicos', 1),
('Sabores do Minho', '500999000', '910999000', 'Regional', 1);

-- Pratos: FastFood (Burger Express)
INSERT INTO Prato (RestauranteId, Nome, Preco, Disponivel) VALUES
((SELECT Id FROM Restaurante WHERE Nome = 'Burger Express'), 'Cheeseburger Clássico', 5.90, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Burger Express'), 'Batata Frita Grande', 3.50, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Burger Express'), 'Menu Duplo Bacon', 8.90, 1);

-- Pratos: Casual (Tasca do Zé)
INSERT INTO Prato (RestauranteId, Nome, Preco, Disponivel) VALUES
((SELECT Id FROM Restaurante WHERE Nome = 'Tasca do Zé'), 'Bitoque com Batata Frita', 7.50, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Tasca do Zé'), 'Bacalhau à Brás', 8.90, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Tasca do Zé'), 'Feijoada de Marisco', 9.50, 1);

-- Pratos: AltaGastronomia (Le Gourmet)
INSERT INTO Prato (RestauranteId, Nome, Preco, Disponivel) VALUES
((SELECT Id FROM Restaurante WHERE Nome = 'Le Gourmet'), 'Robalo com Espuma de Champanhe', 24.90, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Le Gourmet'), 'Tártaro de Atum e Caviar', 22.50, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Le Gourmet'), 'Solomilho Wellington', 28.00, 1);

-- Pratos: Tematicos (Planeta Pizza)
INSERT INTO Prato (RestauranteId, Nome, Preco, Disponivel) VALUES
((SELECT Id FROM Restaurante WHERE Nome = 'Planeta Pizza'), 'Pizza Margherita', 8.00, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Planeta Pizza'), 'Pizza Quatro Queijos', 9.50, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Planeta Pizza'), 'Pizza Pepperoni', 9.00, 1);

-- Pratos: Regional (Sabores do Minho)
INSERT INTO Prato (RestauranteId, Nome, Preco, Disponivel) VALUES
((SELECT Id FROM Restaurante WHERE Nome = 'Sabores do Minho'), 'Rojões à Moda do Minho', 10.90, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Sabores do Minho'), 'Caldo Verde com Broa', 4.50, 1),
((SELECT Id FROM Restaurante WHERE Nome = 'Sabores do Minho'), 'Vitela Assada no Forno', 12.90, 1);