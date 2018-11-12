ALTER SCHEMA breeding RENAME TO cultivation;

ALTER TABLE cultivation.breeding RENAME TO cultivation;

ALTER TABLE cultivation.breeding_comments RENAME TO cultivation_comments;
ALTER TABLE cultivation.cultivation_comments RENAME COLUMN breeding to cultivation;

