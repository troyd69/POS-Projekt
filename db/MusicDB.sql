-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`u_user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`u_user` (
  `u_id` INT NOT NULL AUTO_INCREMENT,
  `u_username` VARCHAR(45) NOT NULL,
  `u_password` VARCHAR(45) NOT NULL,
  `u_birthdate` DATE NULL,
  PRIMARY KEY (`u_id`),
  UNIQUE INDEX `u_username_UNIQUE` (`u_username` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`a_artist`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`a_artist` (
  `a_id` INT NOT NULL AUTO_INCREMENT,
  `a_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`a_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`c_category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`c_category` (
  `c_id` CHAR(2) NOT NULL,
  `c_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`c_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`s_song`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`s_song` (
  `s_id` INT NOT NULL AUTO_INCREMENT,
  `s_titel` VARCHAR(45) NOT NULL,
  `s_a_artist` INT NULL,
  `s_path` VARCHAR(75) NOT NULL,
  `s_timespan` TIME NULL,
  `s_c_category` CHAR(2) NULL,
  PRIMARY KEY (`s_id`),
  UNIQUE INDEX `s_path_UNIQUE` (`s_path` ASC) VISIBLE,
  UNIQUE INDEX `s_titel_UNIQUE` (`s_titel` ASC) VISIBLE,
  INDEX `s_a_artist_idx` (`s_a_artist` ASC) VISIBLE,
  INDEX `s_c_category_idx` (`s_c_category` ASC) VISIBLE,
  CONSTRAINT `s_a_artist`
    FOREIGN KEY (`s_a_artist`)
    REFERENCES `mydb`.`a_artist` (`a_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `s_c_category`
    FOREIGN KEY (`s_c_category`)
    REFERENCES `mydb`.`c_category` (`c_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`p_playlist`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`p_playlist` (
  `p_id` INT NOT NULL AUTO_INCREMENT,
  `p_name` VARCHAR(45) NOT NULL,
  `p_u_user` INT NULL,
  PRIMARY KEY (`p_id`),
  INDEX `p_u_user_idx` (`p_u_user` ASC) VISIBLE,
  CONSTRAINT `p_u_user`
    FOREIGN KEY (`p_u_user`)
    REFERENCES `mydb`.`u_user` (`u_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`i_includes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`i_includes` (
  `i_p_playlist` INT NOT NULL,
  `i_s_song` INT NOT NULL,
  PRIMARY KEY (`i_p_playlist`, `i_s_song`),
  INDEX `i_s_song_idx` (`i_s_song` ASC) VISIBLE,
  CONSTRAINT `i_p_playlist`
    FOREIGN KEY (`i_p_playlist`)
    REFERENCES `mydb`.`p_playlist` (`p_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `i_s_song`
    FOREIGN KEY (`i_s_song`)
    REFERENCES `mydb`.`s_song` (`s_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
