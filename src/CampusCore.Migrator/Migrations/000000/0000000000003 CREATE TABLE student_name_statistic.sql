CREATE TABLE student_name_statistic(
    id uuid NOT NULL,
    name varchar NOT NULL,
    statistic_date date NOT NULL,
    repeat_count int4 default 0 NOT NULL,
    created_at timestamp NOT NULL,
    
    CONSTRAINT student_name_statistic_pk PRIMARY KEY (id),
    CONSTRAINT student_name_statistic_day_name_uq UNIQUE (statistic_date, name)
);
