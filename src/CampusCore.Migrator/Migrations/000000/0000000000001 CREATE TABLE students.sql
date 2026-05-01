CREATE TABLE students (
    id uuid NOT NULL,
    last_name varchar NOT NULL,
    first_name varchar NOT NULL,
    patronymic varchar NULL,
    gender int4 NOT NULL,
    date_of_birth timestamp NOT NULL,
    average_grade numeric(3,2) NOT NULL CHECK (average_grade BETWEEN 2 AND 5),
    special_notes text[],
    group_id uuid NOT NULL,
    
    created_at timestamp NOT NULL,
    updated_at timestamp NULL,
    deleted_at timestamp NULL,
    
    CONSTRAINT students_pk PRIMARY KEY (id)
);
       


