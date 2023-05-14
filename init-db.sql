CREATE USER api_user WITH PASSWORD 'password';
GRANT ALL PRIVILEGES ON DATABASE skill_system TO api_user;
GRANT USAGE, CREATE ON SCHEMA public TO api_user;
