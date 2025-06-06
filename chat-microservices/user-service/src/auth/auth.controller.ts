import { Body, Controller, Post, UsePipes, ValidationPipe } from '@nestjs/common';
import { UsersService } from '../users/users.service';
import { AuthService } from './auth.service';
import { LoginDto } from './dto/login.dto';
import { RegisterDto } from './dto/register.dto';
import { ApiTags } from '@nestjs/swagger';

@ApiTags('auth') 
@Controller('auth')
export class AuthController {
  constructor(
    private usersService: UsersService,
    private authService: AuthService,
  ) {}

  @Post('register') 
  @UsePipes(new ValidationPipe())
  async register(@Body() dto: RegisterDto) {
    return this.usersService.register(dto);
  }

  @Post('login') 
  @UsePipes(new ValidationPipe())
  async login(@Body() dto: LoginDto) {
    return this.authService.login(dto);
  }
}