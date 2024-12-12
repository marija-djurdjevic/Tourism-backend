INSERT INTO payments."Bundles" (
    "Id", "AuthorId", "TourIds", "Price", "Status", "Title"
) VALUES 
(-1, 1, ARRAY[-1, -2, -3], 299.99, 0, 'Title1'),  -- Draft
(-2, 1, ARRAY[-2, -3], 399.99, 1, 'Title2'),      -- Published
(-3, 1, ARRAY[-2, -3], 199.99, 2, 'Title3');      -- Archived