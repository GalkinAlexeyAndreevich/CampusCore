CREATE TABLE student_name_statistic(
    name varchar NOT NULL,
    statistic_date date NOT NULL,
    repeat_count int4 default 0 NOT NULL,
    created_at timestamp NOT NULL,
    
    CONSTRAINT student_name_statistic_pk PRIMARY KEY (statistic_date, name)
);
