-- MySQL Script generated by MySQL Workbench
-- Thu Nov 18 21:30:32 2021
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema study_department
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema study_department
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `study_department` DEFAULT CHARACTER SET utf8 ;
USE `study_department` ;

-- -----------------------------------------------------
-- Table `study_department`.`positions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`positions` (
  `position_id` INT NOT NULL AUTO_INCREMENT,
  `position_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`position_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`access_rights`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`access_rights` (
  `access_right_id` INT NOT NULL AUTO_INCREMENT,
  `access_right_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`access_right_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`qualifications`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`qualifications` (
  `qualification_id` INT NOT NULL AUTO_INCREMENT,
  `qualification_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`qualification_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`workers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`workers` (
  `worker_id` INT NOT NULL AUTO_INCREMENT,
  `worker_first_name` VARCHAR(45) NOT NULL,
  `worker_last_name` VARCHAR(45) NOT NULL,
  `worker_middle_name` VARCHAR(45) NOT NULL,
  `position_id` INT NOT NULL,
  `access_right_id` INT NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `qualification_id` INT NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`worker_id`),
  INDEX `fk_workers_positions_idx` (`position_id` ASC) VISIBLE,
  INDEX `fk_workers_access_rights1_idx` (`access_right_id` ASC) VISIBLE,
  INDEX `fk_workers_qualifications1_idx` (`qualification_id` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE,
  CONSTRAINT `fk_workers_positions`
    FOREIGN KEY (`position_id`)
    REFERENCES `study_department`.`positions` (`position_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_workers_access_rights1`
    FOREIGN KEY (`access_right_id`)
    REFERENCES `study_department`.`access_rights` (`access_right_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_workers_qualifications1`
    FOREIGN KEY (`qualification_id`)
    REFERENCES `study_department`.`qualifications` (`qualification_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`groups`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`groups` (
  `group_id` INT NOT NULL AUTO_INCREMENT,
  `group_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`group_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`partner_companies`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`partner_companies` (
  `partner_company_id` INT NOT NULL AUTO_INCREMENT,
  `company_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`partner_company_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`practices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`practices` (
  `practic_id` INT NOT NULL AUTO_INCREMENT,
  `partner_company_id` INT NOT NULL,
  `practic_address` VARCHAR(45) NOT NULL,
  `practit_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`practic_id`),
  INDEX `fk_practices_has_partner_companies_partner_companies1_idx` (`partner_company_id` ASC) VISIBLE,
  CONSTRAINT `fk_practices_has_partner_companies_partner_companies1`
    FOREIGN KEY (`partner_company_id`)
    REFERENCES `study_department`.`partner_companies` (`partner_company_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`students`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`students` (
  `student_id` INT NOT NULL AUTO_INCREMENT,
  `student_first_name` VARCHAR(45) NOT NULL,
  `student_last_name` VARCHAR(45) NOT NULL,
  `student_middle_name` VARCHAR(45) NOT NULL,
  `group_id` INT NOT NULL,
  `practic_id` INT NULL,
  `graduate_work` VARCHAR(45) NULL,
  `head_graduate_work_worker_id` INT NULL,
  PRIMARY KEY (`student_id`),
  INDEX `fk_students_groups1_idx` (`group_id` ASC) VISIBLE,
  INDEX `fk_students_practices1_idx` (`practic_id` ASC) VISIBLE,
  INDEX `fk_students_workers1_idx` (`head_graduate_work_worker_id` ASC) VISIBLE,
  CONSTRAINT `fk_students_groups1`
    FOREIGN KEY (`group_id`)
    REFERENCES `study_department`.`groups` (`group_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_students_practices1`
    FOREIGN KEY (`practic_id`)
    REFERENCES `study_department`.`practices` (`practic_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_students_workers1`
    FOREIGN KEY (`head_graduate_work_worker_id`)
    REFERENCES `study_department`.`workers` (`worker_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`subjects`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`subjects` (
  `subject_id` INT NOT NULL AUTO_INCREMENT,
  `subject_name` VARCHAR(45) NOT NULL,
  `worker_id` INT NOT NULL,
  PRIMARY KEY (`subject_id`),
  INDEX `fk_subjects_workers1_idx` (`worker_id` ASC) VISIBLE,
  CONSTRAINT `fk_subjects_workers1`
    FOREIGN KEY (`worker_id`)
    REFERENCES `study_department`.`workers` (`worker_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `study_department`.`subjects_has_groups`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `study_department`.`subjects_has_groups` (
  `subject_id` INT NOT NULL,
  `group_id` INT NOT NULL,
  `date` DATETIME NOT NULL,
  PRIMARY KEY (`subject_id`, `group_id`),
  INDEX `fk_subjects_has_groups_groups1_idx` (`group_id` ASC) VISIBLE,
  INDEX `fk_subjects_has_groups_subjects1_idx` (`subject_id` ASC) VISIBLE,
  CONSTRAINT `fk_subjects_has_groups_subjects1`
    FOREIGN KEY (`subject_id`)
    REFERENCES `study_department`.`subjects` (`subject_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_subjects_has_groups_groups1`
    FOREIGN KEY (`group_id`)
    REFERENCES `study_department`.`groups` (`group_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
