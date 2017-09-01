namespace Magus
{
    public class Person : Character
    {

    }
    public class Character
    {
        public string name;
        public short CharacterClass;
        public short x;
        public short y;
        public short strength;
        public short wisdom;
        public short skill;
        public short health;
        public short power;
        public short maxHealth;
        public short maxPower;
        public short canWalk;
        public long xp;
        public short level;
        public List<item> wearing;
        public List<item> wielding;
        public List<item> carrying;
        public short lifeSpan;
        public short backGround;
        public short icon;
        public short speed;
        public short moves;
        public short killerClass;
        public short damageBonus;
        public short ward;
        public short confused;
        public short isPlayer;
        public short isFleeing;
        public short isNasty;
        public short isTracker;
        public short isFooled;
        public short gifts;
        public short slayer;
        public void DisposeItems(List<Item> List)
        {
            List.Empty;
        }

        public void AddItemToList(item i, List<Item> List)
        {
            List.add(i);
        }

        public void RemoveItemFromList(item i, List<Item> List)
        {
            List.remove(i);
        }

        public int HowManyItems(List<Item> List)
        {
            return List.count;
        }

        public void AddThing(thing t)
        {
            t->next = gThings;
            gThings = t;
        }

        void DeleteThing( struct thing * t )
{
  struct thing * tt;
Person* p;

  if (gWorld[t->x][t->y] & CHARACTER_FLAG)
  {
    p = FindCharacter(t->x, t->y);
    if (p)
      p->backGround = t->backGround;
    gWorld[t->x][t->y] &= ~ITEM_FLAG;
    }
  else
    gWorld[t->x][t->y] = t->backGround;

  if (t == gThings)
    gThings = t->next;
  else
  {
    tt = gThings;
    while (tt && tt->next != t)
      tt = tt->next;
    if (tt)
      tt->next = t->next;
  }
  DisposeItems(t->items);
  free(t);
}

void PlaceItem( struct item * item, int x, int y )
{
  struct thing * t;
Person* p;

t = gThings;
  while (t && (t->x != x || t->y != y))
    t = t->next;

  if (t)
    AddItemToList(item, &(t->items));
  else
  {
    t = malloc( sizeof( struct thing));
    t->x = x;
    t->y = y;
    if (gWorld[x][y] & CHARACTER_FLAG)
      t->backGround = FindCharacter(x, y)->backGround;
    else
      t->backGround = gWorld[x][y];
    t->items = NULL;
    AddItemToList(item, &(t->items));
    AddThing(t);
  }

  if (gWorld[x][y] & CHARACTER_FLAG)
  {
    p = FindCharacter(x, y);
    if (p)
      p->backGround = gObjectData[item->id].icon |
				  (p->backGround & 0xFF00) |
				  ITEM_FLAG;
    gWorld[x][y] |= ITEM_FLAG;
  }
  else
    gWorld[x][y] = gObjectData[item->id].icon |
				(gWorld[x][y] & 0xFF00) |
				ITEM_FLAG;
}

struct item * FindItem(int x, int y)
{
  struct thing * t;

t = gThings;
  while (t && (t->x != x || t->y != y))
    t = t->next;
  if (t)
    return t->items;
  else
    return NULL;
}

void RemoveItem( struct item * i )
{
  struct thing * t;
struct item * ip;
Person* p;

t = gThings;
  while (t)
  {
    ip = t->items;
    while (ip)
    {
      if (ip == i)
      {

    RemoveItemFromList(i, &(t->items));
	if (t->items == NULL)

      DeleteThing(t);
	else
	{
	  if (gWorld[t->x][t->y] & CHARACTER_FLAG)
	  {
	    p = FindCharacter(t->x, t->y);
	    if (p)

        p->backGround = gObjectData[t->items->id].icon |
					(p->backGround & 0xFF00) |
					ITEM_FLAG;
	  }
	  else
	    gWorld[t->x][t->y] = gObjectData[t->items->id].icon |
				(gWorld[t->x][t->y] & 0xFF00) |
					ITEM_FLAG;
	}
	return;
      }
      ip = ip->next;
    }
    t = t->next;
  }
}

int MayBeCombined( struct item * i )
{
  switch (gObjectData[i->id].kind)
  {
    case O_CLASS_WEAPON:
    case O_CLASS_MIXEDWEAPON:
      return TRUE;
  }
  return FALSE;
}

int MayUse(Person* p, struct item * it )
{
  int holds, hands;
struct item * i;

holds = 0;
  hands = 2;
  i = p->wielding;
  while (i)
  {
    holds |= gObjectData[i->id].requires;
    hands -= (gObjectData[i->id].requires & 3);
    i = i->next;
  }
  i = p->wearing;
  while (i)
  {
    holds |= gObjectData[i->id].requires;
    hands -= (gObjectData[i->id].requires & 3);
    i = i->next;
  }

  if ((gObjectData[it->id].requires & 3) > hands) return FALSE;

  return ((gObjectData[it->id].requires & 0xFFFC & holds) == 0);
}

void WieldItem(Person* p, struct item * i )
{
  if (p->wielding)
    p->skill -= 5;
  RemoveItemFromList(i, &(p->carrying));
  AddItemToList(i, &(p->wielding));
}

void WearItem(Person* p, struct item * i )
{
  RemoveItemFromList(i, &(p->carrying));
  AddItemToList(i, &(p->wearing));

  switch (i->id)
  {
    case O_GREENAMULET:
      p->skill += 5 + i->modifier;
      break;

    case O_BLUEAMULET:
      p->wisdom += 5 + i->modifier;
      break;

    case O_YELLOWAMULET:
      p->speed += max( 0, 1 + i->modifier/2);
      break;

    case O_REDGLOVES:
      p->strength += 5 + max( 0, i->modifier);
      break;
  }
}

void UnWieldItem(Person* p, struct item * i )
{
  RemoveItemFromList(i, &(p->wielding));
  AddItemToList(i, &(p->carrying));
  if (p->wielding)
    p->skill += 5;
}

void Use(Person* p, struct item * i )
{
  if (i && p->wielding &&
      gObjectData[i->id].kind == O_CLASS_SPELL &&
      gObjectData[p->wielding->id].kind == O_CLASS_SPELL)
    UnWieldItem(p, p->wielding);

  if (!i || !MayUse(p, i)) return;

  switch (gObjectData[i->id].kind)
  {
    case O_CLASS_WEAPON:
    case O_CLASS_THROWINGWEAPON:
    case O_CLASS_RANGEDWEAPON:
    case O_CLASS_SPELL:
    case O_CLASS_MIXEDWEAPON:
    case O_CLASS_SPECIAL:
    case O_CLASS_POTION:
      if (p->wielding)
      {
        if (p->wielding->next)
          return;
        if (!MayBeCombined(p->wielding) || !MayBeCombined(i))
          return;
      }
      WieldItem(p, i);
      break;

    case O_CLASS_ARMOR:
      if (p->moves >= 2)
      {
        WearItem(p, i);
p->moves -= 2;
      }
      break;

    case O_CLASS_GADGET:
      if (p->moves)
      {
        WearItem(p, i);
p->moves--;
      }
      break;
  }
}

void UnWearItem(Person* p, struct item * i )
{
  RemoveItemFromList(i, &(p->wearing));
  AddItemToList(i, &(p->carrying));

  switch (i->id)
  {
    case O_GREENAMULET:
      p->skill -= 5 + i->modifier;
      break;

    case O_BLUEAMULET:
      p->wisdom -= 5 + i->modifier;
      break;

    case O_YELLOWAMULET:
      p->speed -= max( 0, 1 + i->modifier/2);
      break;

    case O_REDGLOVES:
      p->strength -= 5 + max( 0, i->modifier);
      break;
  }
}

void StopUsing(Person* p, struct item * i )
{
  if (!i) return;

  switch (gObjectData[i->id].kind)
  {
    case O_CLASS_SPELL:
    case O_CLASS_THROWINGWEAPON:
    case O_CLASS_MIXEDWEAPON:
      UnWieldItem(p, i);
      break;

    case O_CLASS_WEAPON:
    case O_CLASS_RANGEDWEAPON:
    case O_CLASS_SPECIAL:
    case O_CLASS_POTION:
      if (p->moves)
      {

    UnWieldItem(p, i);
p->moves--;
      }
      break;

    case O_CLASS_ARMOR:
    case O_CLASS_GADGET:
      if (p->moves)
      {

    UnWearItem(p, i);
p->moves--;
      }
      break;
  }
}

/*
void GarbageCollect( void );
{
  struct thing *t;
  Person *p;

  t = gThings;
  while (t)
  {
    p = GetClosestPlayer( t->x, t->y);
    if (!p || max( abs( p->x - t->x), abs( p->y - t->y)) > 25)
    {
      DeleteThing( t);
      return;
    }
    t = t->next;
  }
}
*/

struct item * FindType(short id, struct item * list )
{
  while (list && list->id != id)
    list = list->next;

  return list;
}

struct item * Carries(Person* p, short id)
{
    return FindType(id, p->carrying);
}

struct item * Uses(Person* p, short id)
{
    return FindType(id, p->wielding);
}

struct item * Wears(Person* p, short id)
{
    return FindType(id, p->wearing);
}

Person* MagicallyFindCharacter(int x, int y)
{
    Person* p;

    p = FindCharacter(x, y);
    if (p && !Carries(p, O_NEGATOR))
        return p;
    else
        return NULL;
}

Person* CreateCharacter(char* name, int class, int female, int isPlayer )
{
  Person* pc;
int count;
struct item * i;

pc = malloc( sizeof(Person));
  if (!pc) return NULL;

  memset(pc, 0, sizeof(Person));
  AddCharacter(pc);
  strcpy(pc->name, name);
pc->class = class;
pc->lifeSpan = gClassData[class].lifeSpan;
  pc->maxHealth = gClassData[class].health;
  pc->health = pc->maxHealth;
  pc->maxPower = gClassData[class].power;
  pc->power = pc->maxPower;
  pc->canWalk = gClassData[class].canWalk;
  if (female)
    pc->icon = gClassData[class].femaleIcon;
  else
    pc->icon = gClassData[class].maleIcon;
  pc->speed = gClassData[class].speed;
  pc->moves = pc->speed;
  pc->strength = gClassData[class].strength;
  pc->skill = gClassData[class].skill;
  pc->wisdom = gClassData[class].wisdom;
  pc->isPlayer = isPlayer;

  for (count = 0; count<MAXINVENTORY; count++)
  {
    if (gClassData[class].inventory[count])
    {
      i = malloc( sizeof( struct item));
      i->id = gClassData[class].inventory[count] & 0xFF;
      if (gClassData[class].inventory[count] & 0xFF00)
        i->locked = TRUE;
      else
        i->locked = FALSE;
      if (i->id == O_ARROWS)
        i->modifier = 50;
      else if (!isPlayer)
        i->modifier = Rand( 7) - 3;
      else
        i->modifier = 0;
      i->next = NULL;
        AddItemToList(i, &(pc->carrying));
    }
  }

  return pc;
}

void PlaceCharacter(Person* p, int x, int y)
{
    p->x = x;
    p->y = y;
    p->backGround = gWorld[x][y];
    gWorld[x][y] = (gWorld[x][y] & 0xFF00) | p->icon | CHARACTER_FLAG;
}

void PlaceCharacterCloseTo(Person* p, int xc, int yc, int distance)
{
    int x, y;

    while (TRUE)
    {
        x = xc - distance;
        y = yc - distance;
        while (x <= xc + distance)
        {
            if (Treadable(x, y, p->canWalk))
            {
                PlaceCharacter(p, x, y);
                return;
            }
            x++;
        }
        x = xc - distance;
        y = yc + distance;
        while (x <= xc + distance)
        {
            if (Treadable(x, y, p->canWalk))
            {
                PlaceCharacter(p, x, y);
                return;
            }
            x++;
        }
        x = xc - distance;
        y = yc - distance + 1;
        while (y <= yc + distance - 1)
        {
            if (Treadable(x, y, p->canWalk))
            {
                PlaceCharacter(p, x, y);
                return;
            }
            y++;
        }
        x = xc + distance;
        y = yc - distance + 1;
        while (y <= yc + distance - 1)
        {
            if (Treadable(x, y, p->canWalk))
            {
                PlaceCharacter(p, x, y);
                return;
            }
            y++;
        }
        distance++;
    }
}

void EnterHallOfFame(Person* p);


#define RemoveCharacter(p) gWorld[ (p)->x][ (p)->y] = (p)->backGround
void Death(Person* p, Person* slayer)
{
  struct item * i;
int count;

  RemoveCharacter(p);
  while (p->wielding)
    UnWieldItem(p, p->wielding);

  while (p->wearing)
    UnWearItem(p, p->wearing);

  if (p->isPlayer)
  {
    while (p->carrying)
    {
      i = p->carrying;
      RemoveItemFromList(i, &(p->carrying));
      if (!i->locked)
        PlaceItem(i, p->x, p->y);
      else
        free(i);
    }
  }
  else if (Rand( 10) < 2)
  {
    count = HowManyItems(p->carrying);
count = Rand(count);
i = p->carrying;
    while (i && count)
    {
      i = i->next;
      count--;
    }
    if (i)
    {
      RemoveItemFromList(i, &(p->carrying));
      if (!i->locked)
        PlaceItem(i, p->x, p->y);
      else
        free(i);
    }
  }
  p->health = 0;
  p->maxHealth = 0;
  p->moves = 0;

  if (slayer && !p->isPlayer && !p->isNasty)
  {
    Message( "That wasn't very nice!");
i = malloc( sizeof( struct item));
    i->id = O_CHAINANDBALL;
    i->modifier = 0;
    i->locked = TRUE;
    i->next = NULL;
    AddItemToList(i, &(slayer->carrying));
  }
  if (p->isPlayer && !p->lifeSpan)
  {
    p->slayer = slayer->class;
    EnterHallOfFame(p);
  }
}

int Load(Person* p)
{
    int load;
  struct item * i;

load = 0;
  i = p->wielding;
  while (i)
  {
    load += gObjectData[i->id].weight;
    i = i->next;
  }
  i = p->wearing;
  while (i)
  {
    load += gObjectData[i->id].weight;
    i = i->next;
  }
  i = p->carrying;
  while (i)
  {
    load += gObjectData[i->id].weight;
    i = i->next;
  }
  return load;
}

int ActualLoad(Person* p)
{
    int load;
  struct item * i;

load = Load(p);
  if ((i = Carries(p, O_BUBBLE)) != NULL)
    load -= 6 + i->modifier;
  return load;
}

int OverLoaded(Person* p)
{
    return (ActualLoad(p) > p->strength);
}

/*
  TRUE  -> character still in action
  FALSE -> character should be disposed of
*/

int UpdateCharacter(Person* p)
{
  struct item * i;
int load;

  if (p->health <= 0) return FALSE;

  if (p->power<p->maxPower)
  {
    p->power += 2;
    p->power += min(p->moves, p->speed);

    if ((i = Wears(p, O_BLUERING)) != 0)
      p->power += 4 + i->modifier;
    if (p->power > p->maxPower)
      p->power = p->maxPower;
  }

  if (p->health<p->maxHealth)
  {
    p->health++;
    p->health += min(p->moves, p->speed);

    if ((i = Wears(p, O_GREENRING)) != 0)
      p->health += 2 + i->modifier;
    if (p->health > p->maxHealth)
      p->health = p->maxHealth;
  }
  p->canWalk = gClassData[p->class].canWalk;
  p->moves = p->speed;

  load = ActualLoad(p);
  if (load > p->strength*2)
    p->moves = 0;
  else if (load > p->strength + p->strength/2)
    p->moves = p->speed/3;
  else if (load > p->strength)
    p->moves -= p->speed/3;

  if (p->damageBonus)
    p->damageBonus--;
  p->confused = 0;
  if (p->ward)
    p->ward--;

  if ((p->canWalk & (1 << ((p->backGround >> 8) & 31))) == 0)
  {
    p->health -= 10;
    if (p->health <= 0)
      Death(p, NULL);
  }

  if (p->lifeSpan)
  {
    p->lifeSpan--;
    if (!p->lifeSpan)
    {
      RemoveCharacter(p);
      return FALSE;
    }
  }

  return TRUE;
}

int IsWounded(Person* p)
{
    if (gClassData[p->class].rank == 4711)
    return FALSE;

  if (p->health<p->maxHealth/3) return TRUE;

  if (p->health > (2* p->maxHealth)/3) return FALSE;

  return (int) p->isFleeing;
}
    }
}