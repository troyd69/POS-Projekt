-- MySQL Workbench Forward Engineering
DROP DATABASE if exists musicdb;

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema MusicDB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema MusicDB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `MusicDB` DEFAULT CHARACTER SET utf8 ;
USE `MusicDB` ;


-- -----------------------------------------------------
-- Table `MusicDB`.`u_user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MusicDB`.`u_user` (
  `u_id` INT NOT NULL AUTO_INCREMENT,
  `u_username` VARCHAR(45) NOT NULL,
  `u_password` VARCHAR(45) NOT NULL,
  `u_birthdate` DATE NULL,
  PRIMARY KEY (`u_id`),
  UNIQUE INDEX `u_username_UNIQUE` (`u_username` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MusicDB`.`a_artist`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MusicDB`.`a_artist` (
  `a_id` INT NOT NULL AUTO_INCREMENT,
  `a_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`a_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MusicDB`.`c_category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MusicDB`.`c_category` (
  `c_id` CHAR(2) NOT NULL,
  `c_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`c_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MusicDB`.`s_song`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MusicDB`.`s_song` (
  `s_id` INT NOT NULL AUTO_INCREMENT,
  `s_titel` VARCHAR(45) NOT NULL,
  `s_a_artist` INT NULL,
  `s_path` VARCHAR(75) NOT NULL,
  `s_timespan` TIME NULL,
  `s_c_category` CHAR(2) NULL,
  PRIMARY KEY (`s_id`),
  UNIQUE INDEX `s_path_UNIQUE` (`s_path` ASC),
  UNIQUE INDEX `s_titel_UNIQUE` (`s_titel` ASC),
  INDEX `s_a_artist_idx` (`s_a_artist` ASC),
  INDEX `s_c_category_idx` (`s_c_category` ASC),
  CONSTRAINT `s_a_artist`
    FOREIGN KEY (`s_a_artist`)
    REFERENCES `MusicDB`.`a_artist` (`a_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `s_c_category`
    FOREIGN KEY (`s_c_category`)
    REFERENCES `MusicDB`.`c_category` (`c_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MusicDB`.`p_playlist`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MusicDB`.`p_playlist` (
  `p_id` INT NOT NULL AUTO_INCREMENT,
  `p_name` VARCHAR(45) NOT NULL,
  `p_u_user` INT NULL,
  PRIMARY KEY (`p_id`),
  INDEX `p_u_user_idx` (`p_u_user` ASC),
  CONSTRAINT `p_u_user`
    FOREIGN KEY (`p_u_user`)
    REFERENCES `MusicDB`.`u_user` (`u_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MusicDB`.`i_includes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MusicDB`.`i_includes` (
  `i_p_playlist` INT NOT NULL,
  `i_s_song` INT NOT NULL,
  PRIMARY KEY (`i_p_playlist`, `i_s_song`),
  INDEX `i_s_song_idx` (`i_s_song` ASC),
  CONSTRAINT `i_p_playlist`
    FOREIGN KEY (`i_p_playlist`)
    REFERENCES `MusicDB`.`p_playlist` (`p_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `i_s_song`
    FOREIGN KEY (`i_s_song`)
    REFERENCES `MusicDB`.`s_song` (`s_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


insert into a_artist values
(null, 'David Guetta'),
(null, 'Lil Peep'),
(null, 'Travis Scott'),
(null, 'Drake'),
(null, 'Dat Adam'),
(null, 'Post Malone'),
(null, 'Dua lipa'),
(null, 'Future'),
(null, 'Ed Sheeran'),
(null, 'Harry Styles'),
(null, 'Justin Bieber'),
(null, 'Imagine Dragons'),
(null, 'NLE Choppa');
         
insert into c_category values
('HP', 'Hip-Hop'),
('PO', 'Pop'),
('DE', 'Dance/Electronic'),
('CC', 'Classical');

insert into u_user values
(null, 'Ali', '1234', '2005-06-26'),
(null, 'Nikii', '1234', '2005-02-12'),
(null, 'Mathias', '1234', '2005-08-10');

insert into s_song values
(null, 'Star Shopping', 2, 'StarShopping', '00:02:22', 'HP'),
(null, 'Crazy What Love Can Do', 1, 'Crazy What Love Can Do (Lyric Video)', '00:02:49', 'DE'),
(null, 'SICKO MODE', 3, 'Travis Scott - SICKO MODE (Lyrics) ft. Drake', '00:05:12', 'HP'),
(null, 'Wants and Needs (Feat. Lil Baby)', 4, 'Drake - Wants and Needs (Lyrics) ft. Lil Baby', '00:03:12', 'HP'),
(null, 'Rockstar', 5, 'Post_Malone_-_rockstar_ft._21_Savage', '00:04:03', 'PO'),
(null, 'Circles', 5, 'Post_Malone_-_Circles', '00:04:03', 'HP'),
(null, 'One Kiss', 6, 'Calvin_Harris_Dua_Lipa_-_One_Kiss_Official_Video', '00:04:03', 'HP'),
(null, 'Levitating', 6, 'Dua_Lipa_-_Levitating_Featuring_DaBaby_Official_Music_Video', '00:04:03', 'HP'),
(null, 'Pushin', 7, 'Gunna_Future_-_pushin_p._feat._Young_Thug_Official_Audio', '00:04:03', 'HP'),
(null, 'Wait for U', 7, 'Future_-_WAIT_FOR_U_Official_Music_Video_ft._Drake_Tems', '00:04:03', 'HP'),
(null, 'Shape of you', 8, 'Ed_Sheeran_-_Shape_of_You_Official_Video', '00:04:03', 'HP'),
(null, 'Perfect', 8, 'Ed_Sheeran_-_Perfect_Official_Music_Video', '00:04:03', 'HP'),
(null, 'Watermelon Sugar', 9, 'Harry_Styles_-_Watermelon_Sugar_Official_Video', '00:04:03', 'HP'),
(null, 'As it was', 9, 'Harry_Styles_-_As_It_Was_Lyrics', '00:04:03', 'HP'),
(null, 'Love Yourself', 10, 'Justin_Bieber_-_Love_Yourself_PURPOSE_The_Movement', '00:04:03', 'HP'),
(null, 'Believer', 11, 'Imagine Dragons - Believer', '00:04:03', 'HP'),
(null, 'Enemy', 11, 'Imagine Dragons x J.I.D - Enemy (Lyrics)', '00:04:03', 'HP'),
(null, 'Shotta Flow', 12, 'NLE_Choppa_-_Shotta_Flow_Remix_ft._Blueface_Dir._by__ColeBennett_', '00:04:03', 'HP');

insert into p_playlist values
(null, 'Nikis Playlist', 2),
(null, 'Mathias Playlist', 3),
(null, 'Alis Playlist', 1);

insert into i_includes values
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(2, 3),
(2, 4),
(2, 5);

select *
from u_user;

select *
from u_user
where u_username like 'Nikii';

select *
from p_playlist;

select *
from s_song;