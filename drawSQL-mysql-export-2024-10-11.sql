CREATE TABLE `Orders`(
    `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `order_date` DATETIME NOT NULL,
    `user_id` BIGINT UNSIGNED NOT NULL,
    `status` VARCHAR(255) NOT NULL
);
ALTER TABLE
    `Orders` ADD INDEX `orders_user_id_index`(`user_id`);
CREATE TABLE `Categories`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(255) NOT NULL
);
CREATE TABLE `user`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `username` VARCHAR(255) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `created_at` DATETIME NOT NULL
);
CREATE TABLE `Order_items`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `order_id` INT UNSIGNED NOT NULL,
    `product_id` INT UNSIGNED NOT NULL,
    `price_at_order` FLOAT(53) NOT NULL,
    `quantity` SMALLINT NOT NULL
);
CREATE TABLE `Products`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(255) NOT NULL,
    `description` TEXT NOT NULL,
    `image_url` VARCHAR(255) NOT NULL,
    `price` DECIMAL(10, 2) NOT NULL,
    `category_id` INT UNSIGNED NOT NULL
);
ALTER TABLE
    `Products` ADD CONSTRAINT `products_category_id_foreign` FOREIGN KEY(`category_id`) REFERENCES `Categories`(`id`);
ALTER TABLE
    `Order_items` ADD CONSTRAINT `order_items_product_id_foreign` FOREIGN KEY(`product_id`) REFERENCES `Products`(`id`);
ALTER TABLE
    `Order_items` ADD CONSTRAINT `order_items_order_id_foreign` FOREIGN KEY(`order_id`) REFERENCES `Orders`(`id`);
ALTER TABLE
    `Orders` ADD CONSTRAINT `orders_user_id_foreign` FOREIGN KEY(`user_id`) REFERENCES `user`(`id`);