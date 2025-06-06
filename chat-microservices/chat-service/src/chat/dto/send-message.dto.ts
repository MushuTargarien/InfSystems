import { ApiProperty } from '@nestjs/swagger';
import { IsNotEmpty, IsString, IsNumber } from 'class-validator';

export class SendMessageDto {
  @ApiProperty({ example: 'Привет, как дела?' })
  @IsNotEmpty()
  @IsString()
  content: string;

  @ApiProperty({ example: 123, description: 'ID пользователя-отправителя' })
  @IsNotEmpty()
  @IsNumber()
  senderId: number;

  @ApiProperty({ example: 'general', required: false, description: 'Название комнаты или чата' })
  room?: string;
}