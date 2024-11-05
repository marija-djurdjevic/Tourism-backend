INSERT INTO tours."Tour"(
    "Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo",
    "PublishedAt", "ArchivedAt", "AverageScore")
VALUES 
    (-1, 1, 'Leotar vertical', 'Vertical race to the top of the mountain', 0, '#hiking', 1500, 0, '{{"type": 0, "time": 60, "distance": 10.0}}',
    '2024-10-15 14:15:00', '2024-10-15 14:15:00', 4.5);
	
INSERT INTO tours."Tour"(
    "Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo",
    "PublishedAt", "ArchivedAt", "AverageScore")
VALUES 
    (-2, 1, 'Exploring nature', 'Walk through the forest with a guide', 0, '#nature', 730, 0, '{{"type": 1, "time": 30, "distance": 5.0}}',
    '2024-10-15 14:15:00', '2024-10-15 14:15:00', 4.0);
	
INSERT INTO tours."Tour"(
    "Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo",
    "PublishedAt", "ArchivedAt", "AverageScore")
VALUES 
    (-3, 1, 'Bird perspective', 'Helicopter tour over the city with panoramic views of famous landmarks', 0, '#flying', 5000, 0, '{{"type": 2, "time": 15, "distance": 2.0}}',
    '2024-10-15 14:15:00', '2024-10-15 14:15:00', 5.0);
    