#include <stdio.h>


main()
{
	int fahr;
	int ky;

	for (fahr = 300; fahr >= 0; fahr = fahr - 20)
	{
		printf(fahr);

		for (ky = 300; ky >= 0; ky = ky - 20)
		{
			printf(ky);
		}
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