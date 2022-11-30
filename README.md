# DateAppPC.EXMPL
Простое нативное приложение для Windows с демонстрацией ООП и SQL баз данных на примере приложения для знакомств.
Все данные хранятся в SQL базе данных, по поводу неё ниже в справке.

Работа программы разделена на две логические части:

- Обработка\хранение\сопоставление результатов главного теста, конструктор которого находится в папке Quest.

- Хранение данных полльзователей, их взаимодействие и подбор по пораметрам.

Справка:

  - SQL:
  
    - Для работы программы необходима база данных SQL
    
    - Скачать необходимые программы можно тут: https://www.pgadmin.org/download/pgadmin-4-windows/
    
    - После необходимо создать таблицу:
    
        Код:
        
        create table users1 (
        
          user_id integer primary key not null,
          
          user_name varchar(64) not null,
          
          user_surname varchar(64),
          
          user_patronymic varchar(64),
          
          user_age integer not null,
          
          user_sex varchar(64),
          
          user_temperament varchar(64),
          
          user_character varchar(64),
          
          user_login varchar(64) not null,
          
          user_password varchar(64) not null,
          
          user_nick varchar(64) not null,
          
          user_favorite varchar(1024),
          
          user_picture varchar(256) not null,
          
          user_role varchar(64),
          
          user_interests varchar(1024),
          
          user_info varchar(1024),
          
          user_type varchar(64)
          
        )
