-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 10, 2025 at 03:08 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `inventorydb`
--

-- --------------------------------------------------------

--
-- Table structure for table `keperluansumurs`
--

CREATE TABLE `keperluansumurs` (
  `IdKeperluans` int(11) NOT NULL,
  `IdSumur` int(11) NOT NULL,
  `KodeMaterial` varchar(255) NOT NULL,
  `DeskripsiMaterial` longtext NOT NULL,
  `BaseUnit` longtext NOT NULL,
  `Jumlah` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `keperluansumurs`
--

INSERT INTO `keperluansumurs` (`IdKeperluans`, `IdSumur`, `KodeMaterial`, `DeskripsiMaterial`, `BaseUnit`, `Jumlah`) VALUES
(28, 1, 'MAT001', 'Semen', 'SAK', 5),
(29, 1, 'MAT003', 'Pasir', 'Sak', 15),
(30, 1, 'MAT004', 'Pipa 3\"', 'pcs', 2);

-- --------------------------------------------------------

--
-- Table structure for table `po_barang`
--

CREATE TABLE `po_barang` (
  `id_pobarang` int(11) NOT NULL,
  `kode_ro` int(11) NOT NULL,
  `vendor` char(255) NOT NULL,
  `kode_gudang` char(50) NOT NULL,
  `kode_material` char(50) NOT NULL,
  `deskripsi_material` char(50) NOT NULL,
  `base_unit` char(50) NOT NULL,
  `jumlah` int(11) NOT NULL,
  `harga_per_unit` int(11) NOT NULL,
  `eta_date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `stockbarangs`
--

CREATE TABLE `stockbarangs` (
  `IdBarang` int(11) NOT NULL,
  `KodeGudang` longtext NOT NULL,
  `KodeMaterial` varchar(255) NOT NULL,
  `DeskripsiMaterial` longtext NOT NULL,
  `BaseUnit` longtext NOT NULL,
  `Jumlah` int(11) NOT NULL,
  `HargaPerUnit` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `stockbarangs`
--

INSERT INTO `stockbarangs` (`IdBarang`, `KodeGudang`, `KodeMaterial`, `DeskripsiMaterial`, `BaseUnit`, `Jumlah`, `HargaPerUnit`) VALUES
(1, 'GD001', 'MAT001', 'Semen', 'SAK', 5, 95000),
(2, 'GD002', 'MAT002', 'Lem 25L', 'Botol', 20, 55000),
(3, 'GD003', 'MAT003', 'Pasir', 'Sak', 35, 100000),
(4, 'GD004', 'MAT004', 'Pipa 3\"', 'pcs', 28, 100000);

-- --------------------------------------------------------

--
-- Table structure for table `sumurs`
--

CREATE TABLE `sumurs` (
  `IdSumur` int(11) NOT NULL,
  `DaerahSumur` longtext NOT NULL,
  `NamaSumur` longtext NOT NULL,
  `StartDate` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sumurs`
--

INSERT INTO `sumurs` (`IdSumur`, `DaerahSumur`, `NamaSumur`, `StartDate`) VALUES
(1, 'Jakarta', 'JAK-01', '2025-02-05 00:00:00.000000'),
(2, 'Jakarta', 'JAK-02', '2025-02-12 00:00:00.000000'),
(3, 'Jambi', 'JBI-01', '2025-02-28 00:00:00.000000'),
(4, 'Jambi', 'JBI-02', '2025-02-19 00:00:00.000000');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250128031512_InitialCreate', '8.0.2'),
('20250128041512_UpdateRelasiSumurKeperluanSumur', '8.0.2'),
('20250128133612_FixKeperluanSumur', '8.0.2'),
('20250205031603_FixKeperluanSumurv2', '8.0.2'),
('20250205033332_FixKeperluanSumurRelation', '8.0.2');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `keperluansumurs`
--
ALTER TABLE `keperluansumurs`
  ADD PRIMARY KEY (`IdKeperluans`),
  ADD KEY `IX_KeperluanSumurs_IdSumur` (`IdSumur`),
  ADD KEY `IX_KeperluanSumurs_KodeMaterial` (`KodeMaterial`);

--
-- Indexes for table `po_barang`
--
ALTER TABLE `po_barang`
  ADD PRIMARY KEY (`id_pobarang`);

--
-- Indexes for table `stockbarangs`
--
ALTER TABLE `stockbarangs`
  ADD PRIMARY KEY (`IdBarang`),
  ADD UNIQUE KEY `AK_StockBarangs_KodeMaterial` (`KodeMaterial`);

--
-- Indexes for table `sumurs`
--
ALTER TABLE `sumurs`
  ADD PRIMARY KEY (`IdSumur`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `keperluansumurs`
--
ALTER TABLE `keperluansumurs`
  MODIFY `IdKeperluans` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT for table `po_barang`
--
ALTER TABLE `po_barang`
  MODIFY `id_pobarang` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `stockbarangs`
--
ALTER TABLE `stockbarangs`
  MODIFY `IdBarang` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `sumurs`
--
ALTER TABLE `sumurs`
  MODIFY `IdSumur` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `keperluansumurs`
--
ALTER TABLE `keperluansumurs`
  ADD CONSTRAINT `FK_KeperluanSumurs_StockBarangs_KodeMaterial` FOREIGN KEY (`KodeMaterial`) REFERENCES `stockbarangs` (`KodeMaterial`),
  ADD CONSTRAINT `FK_KeperluanSumurs_Sumurs_IdSumur` FOREIGN KEY (`IdSumur`) REFERENCES `sumurs` (`IdSumur`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
