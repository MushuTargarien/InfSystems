import { Injectable, ConflictException, UnauthorizedException } from '@nestjs/common';
import { UsersService } from '../users/users.service';
import { JwtService } from '@nestjs/jwt';
import { LoginDto } from './dto/login.dto';

@Injectable()
export class AuthService {
  constructor(
    private usersService: UsersService,
    private jwtService: JwtService,
  ) {}

  async validateUser(email: string, pass: string) {
    const user = await this.usersService.validateUser(email, pass);
    if (!user) {
      return null;
    }
    return {
      userId: user.id,
      email: user.email,
    };
  }

 async login(loginDto: LoginDto) {
  const user = await this.validateUser(loginDto.email, loginDto.password);
  
  if (!user) { // ✅ Проверяем, что пользователь существует
    throw new UnauthorizedException('Неверные учетные данные');
  }

  const payload = {
    email: user.email,
    sub: user.userId,
  };

  return {
    access_token: this.jwtService.sign(payload),
  };
}
}