CREATE DATABASE IF NOT EXISTS  WordPress character set = 'utf8' collate = 'utf8_general_ci';
use WordPress;
grant all on WordPress.* to 'robustuser'@'localhost';
quit
