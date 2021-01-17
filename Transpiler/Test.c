#include <stdio.h>


main()
{
	int fahr;

	for (fahr = 300; fahr >= 0; fahr = fahr - 20)
	{
		printf(fahr);
	}

	while (fahr < 300)
	{
		fahr = fahr + 10;

		if (fahr > 0)
		{
			printf(fahr);
		}
	}
}