CREATE TABLE IF NOT EXISTS messages(
    id SERIAL PRIMARY KEY,
    message_body VARCHAR(125) NOT NULL,
    published_at timestamp with time zone NOT NULL
);