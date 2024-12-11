CREATE TABLE `Category`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(255) NOT NULL
);
CREATE TABLE `Order`(
    `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `OrderDate` DATETIME NOT NULL,
    `DeliveryDate` DATETIME NOT NULL,
    `Status` LINESTRING NOT NULL,
    `UserId` BIGINT NOT NULL,
    `User` INT NOT NULL
);
CREATE TABLE `Product`(
    `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(255) NOT NULL,
    `Description` VARCHAR(255) NOT NULL,
    `PhotoUrl` MULTILINESTRING NOT NULL,
    `Price` DECIMAL(8, 2) NOT NULL,
    `CategoryId` INT NOT NULL,
    `Catgegory` BIGINT NOT NULL
);
CREATE TABLE `User`(
    `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `User` LINESTRING NOT NULL,
    `Email` LINESTRING NOT NULL
);
CREATE TABLE `OrderProduct`(
    `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `OrderId` BIGINT NOT NULL,
    `Order` BIGINT NOT NULL,
    `ProductId` BIGINT NOT NULL,
    `Product` BIGINT NOT NULL,
    `PriceAtOrderTime` DECIMAL(8, 2) NOT NULL
);
ALTER TABLE
    `Order` ADD CONSTRAINT `order_userid_foreign` FOREIGN KEY(`UserId`) REFERENCES `User`(`id`);
ALTER TABLE
    `OrderProduct` ADD CONSTRAINT `orderproduct_productid_foreign` FOREIGN KEY(`ProductId`) REFERENCES `Product`(`id`);
ALTER TABLE
    `Product` ADD CONSTRAINT `product_categoryid_foreign` FOREIGN KEY(`CategoryId`) REFERENCES `Category`(`id`);
ALTER TABLE
    `OrderProduct` ADD CONSTRAINT `orderproduct_orderid_foreign` FOREIGN KEY(`OrderId`) REFERENCES `Order`(`id`);