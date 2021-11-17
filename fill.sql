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
("Математика", 2),
("Физика", 2),
("Химия", 2);

insert into classes_char (char_name) values
("А"),
("Б"),
("В"),
("Г"),
("Д"),
("Е"),
("Ж"),
("З"),
("И");

insert into classes_type (type_number) values
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10),
(11);

insert into classes (class_type_id, classroom_teacher_worker_id, class_char_id ) values
(1, 1, 2),
(10, 1, 2),
(2, 1, 2);

insert into students (last_name, first_name, middle_name, class_id) values
("Иванов", "Иван", "Иванович", 1),
("Александров", "Александр", "Александрович", 2);


insert into schedules (date, class_id, subject_id) values
("2021-11-12 09:00:00", 1, 1),
("2021-11-11 09:00:00", 1, 2),
("2021-11-9 09:00:00", 1, 3);
