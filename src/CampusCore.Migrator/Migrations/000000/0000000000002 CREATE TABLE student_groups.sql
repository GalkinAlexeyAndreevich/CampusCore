CREATE TABLE student_groups(
   id uuid NOT NULL,
   name varchar NOT NULL,
   abbreviation varchar NOT NULL,
   training_format varchar NOT NULL,
   study_start_year int4 NOT NULL,
   study_end_year int4 NOT NULL,

   created_at timestamp NOT NULL,
   updated_at timestamp NULL,
   deleted_at timestamp NULL,

   CONSTRAINT student_groups_pk PRIMARY KEY (id)
)
