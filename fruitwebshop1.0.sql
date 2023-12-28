-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2023. Dec 27. 13:20
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `fruitwebshop`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `fruits`
--

CREATE TABLE `fruits` (
  `FruitId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text DEFAULT NULL,
  `Price` decimal(10,2) NOT NULL,
  `StockQuantity` int(11) NOT NULL,
  `ImageUrl` varchar(255) DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `fruits`
--

INSERT INTO `fruits` (`FruitId`, `Name`, `Description`, `Price`, `StockQuantity`, `ImageUrl`, `CreatedAt`, `UpdatedAt`) VALUES
(1, 'Apple', 'Fresh and crispy apple', 1.99, 100, 'apple.jpg', '2023-12-27 11:59:05', '2023-12-27 11:59:05'),
(2, 'Banana', 'Ripe and sweet banana', 0.99, 150, 'banana.jpg', '2023-12-27 11:59:05', '2023-12-27 11:59:05'),
(3, 'Orange', 'Juicy and tangy orange', 2.49, 120, 'orange.jpg', '2023-12-27 11:59:05', '2023-12-27 11:59:05'),
(4, 'Grapes', 'Sweet and seedless grapes', 3.99, 80, 'grapes.jpg', '2023-12-27 11:59:05', '2023-12-27 11:59:05');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `orderitems`
--

CREATE TABLE `orderitems` (
  `OrderItemId` int(11) NOT NULL,
  `OrderId` int(11) DEFAULT NULL,
  `FruitId` int(11) DEFAULT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `orderitems`
--

INSERT INTO `orderitems` (`OrderItemId`, `OrderId`, `FruitId`, `Quantity`, `Price`) VALUES
(1, 1, 1, 3, 5.97),
(2, 1, 2, 2, 1.98),
(3, 2, 3, 1, 2.49),
(4, 2, 4, 2, 7.98);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `orders`
--

CREATE TABLE `orders` (
  `OrderId` int(11) NOT NULL,
  `UserId` int(11) DEFAULT NULL,
  `TotalAmount` decimal(10,2) NOT NULL,
  `OrderDate` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `orders`
--

INSERT INTO `orders` (`OrderId`, `UserId`, `TotalAmount`, `OrderDate`) VALUES
(1, 1, 7.95, '2023-01-01 09:00:00'),
(2, 2, 4.98, '2023-01-02 13:30:00');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `UserId` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`UserId`, `Username`, `PasswordHash`, `Email`, `CreatedAt`) VALUES
(1, 'john_doe', 'hashed_password_1', 'john.doe@example.com', '2023-12-27 12:10:32'),
(2, 'jane_smith', 'hashed_password_2', 'jane.smith@example.com', '2023-12-27 12:10:32'),
(3, 'admin', 'hashed_password_admin', 'admin@example.com', '2023-12-27 12:10:32');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `fruits`
--
ALTER TABLE `fruits`
  ADD PRIMARY KEY (`FruitId`);

--
-- A tábla indexei `orderitems`
--
ALTER TABLE `orderitems`
  ADD PRIMARY KEY (`OrderItemId`),
  ADD KEY `OrderId` (`OrderId`),
  ADD KEY `FruitId` (`FruitId`);

--
-- A tábla indexei `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`OrderId`),
  ADD UNIQUE KEY `UserId` (`UserId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `fruits`
--
ALTER TABLE `fruits`
  MODIFY `FruitId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `orderitems`
--
ALTER TABLE `orderitems`
  MODIFY `OrderItemId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `orders`
--
ALTER TABLE `orders`
  MODIFY `OrderId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `orderitems`
--
ALTER TABLE `orderitems`
  ADD CONSTRAINT `orderitems_ibfk_1` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`OrderId`) ON DELETE CASCADE,
  ADD CONSTRAINT `orderitems_ibfk_2` FOREIGN KEY (`FruitId`) REFERENCES `fruits` (`FruitId`) ON DELETE CASCADE;

--
-- Megkötések a táblához `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
