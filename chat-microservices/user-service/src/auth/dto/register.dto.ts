import { ApiProperty } from '@nestjs/swagger';
import { IsEmail, IsNotEmpty, MinLength, Matches } from 'class-validator';

export class RegisterDto {
  @ApiProperty({ example: 'user@example.com' })
  @IsEmail()
  email: string;

  @ApiProperty({
    example: 'StrongPass1',
    description: 'Пароль должен состоять минимум из 8 символов и содержать хотя бы одну заглавную букву',
  })
  @IsNotEmpty()
  @MinLength(8)
  @Matches(/^(?=.*[A-Z]).{8,}$/, {
    message: 'Пароль должен быть длиной не менее 8 символов и содержать хотя бы одну заглавную букву',
  })
  password: string;

  @ApiProperty({ example: 'John Doe', required: false })
  name?: string;
}