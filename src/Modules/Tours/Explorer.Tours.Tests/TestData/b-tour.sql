INSERT INTO tours."Tour"(
    "Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo",
    "PublishedAt", "ArchivedAt", "AverageScore")
VALUES 
    (-1, 1, 'Leotar vertical', 'Vertical race to the top of the mountain', 0, '{{#hiking, #adventure}}', 1500, 0, '{{"Type": 0, "Time": 60, "Distance": 10.0}}',
    '2024-10-15 14:15:00', '2024-10-15 14:15:00', 4.5);
	
INSERT INTO tours."Tour"(
    "Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo",
    "PublishedAt", "ArchivedAt", "AverageScore")
VALUES 
    (-2, 1, 'Exploring nature', 'Walk through the forest with a guide', 0, '{{#mountain, #lake}}', 730, 1, '{{"Type": 1, "Time": 30, "Distance": 5.0}}',
    '2024-10-15 14:15:00', '2024-10-15 14:15:00', 4.0);
	
INSERT INTO tours."Tour"(
    "Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo",
    "PublishedAt", "ArchivedAt", "AverageScore")
VALUES 
    (-3, 1, 'Bird perspective', 'Helicopter tour over the city with panoramic views of famous landmarks', 0, '{{#sky, #fly}}', 5000, 1, '{{"Type": 2, "Time": 15, "Distance": 2.0}}',
    '2024-10-15 14:15:00', '2024-10-15 14:15:00', 5.0);