use study_department;

insert into access_rights (access_right_name) values
("Администратор"),
("Сотрудник учебного отдела"),
("Преподаватель");


insert into positions (position_name) values
("Декан"),
("Секретарь"),
("Преподаватель");

insert into qualifications (qualification_name) values
("Профессор по базам данных"),
("Магистр технических наук");

insert into workers (worker_last_name, worker_first_name, worker_middle_name, email, password, position_id, qualification_id, access_right_id) values
("Ковалевский", "Артем", "Александрович", "kov@bmku.by", "1234", 1, 1, 1);


insert into subjects (subject_name, worker_id) values
("Математика", 1),
("Физика", 1),
("Химия", 1);


insert into study_department.groups (group_name) values
("913801");

insert into partner_companies (company_name) values
("тест");


insert into students (student_last_name, student_first_name, student_middle_name, group_id, practic_id) values
("Иванов", "Иван", "Иванович", 1, 1);


insert into subjects_has_groups (date_time_start, date_time_end, group_id, subject_id) values
("2021-11-12 09:00:00", "2021-11-12 10:00:00", 1, 1),
("2021-12-12 09:00:00", "2021-12-12 10:00:00", 1, 1),
("2021-11-11 09:00:00", "2021-11-11 10:00:00", 1, 2),
("2021-11-9 09:00:00", "2021-11-9 10:00:00", 1, 3);
