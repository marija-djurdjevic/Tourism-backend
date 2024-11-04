INSERT INTO tours."Tour" (
	"Id", "AuthorId", "Name", "Description", "Difficulty", "Tags", "Price", "Status", "TransportInfo", "PublishedAt", "ArchivedAt", "AverageScore"
) VALUES
(
	2, 2, 'Desert Adventure', 'Experience the vast and mystical desert landscapes.', 2, 'adventure,desert', 120.00, 1, 
	'{{"Time": 10, "Distance": 30.2, "Transport": 2}}', '2023-07-20 08:00:00', '2023-07-20 08:00:00', 4.7
),
(
	3, 2, 'City Lights', 'A guided tour through the vibrant city life and historical sites.', 1, 'city,history', 80.00, 1, 
	'{{"Time": 5, "Distance": 10.0, "Transport": 0}}', '2023-05-10 14:00:00', '2023-05-10 14:00:00', 4.2
),
(
	4, 2, 'Forest Escape', 'A peaceful getaway into the deep forest trails.', 3, 'forest,escape', 200.00, 1,
	'{{"Time": 20, "Distance": 60.0, "Transport": 1}}', '2023-08-01 09:00:00', '2023-08-01 09:00:00', 4.9
),
(
	5, 2, 'Seaside Stroll', 'Enjoy the fresh ocean breeze along the seaside trails.', 1, 'beach,relax', 60.00, 1, 
	'{{"Time": 8, "Distance": 15.0, "Transport": 0}}', '2023-06-18 16:00:00', '2023-06-18 16:00:00', 4.1
),
(
	6, 2, 'Historic Village', 'Step back in time in this historic village tour.', 2, 'history,village', 90.00, 1, 
	'{{"Time": 12, "Distance": 25.3, "Transport": 2}}', '2023-09-10 11:00:00', '2023-09-10 11:00:00', 4.4
);