import { Controller, Get, Param } from '@nestjs/common';
import { UsersService } from './users.service';
import { ApiTags } from '@nestjs/swagger';

@ApiTags('users') 
@Controller('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Get(':id') // Аннотация для GET-запроса
  async getUser(@Param('id') id: string) {
    return this.usersService.findById(+id);
  }
}