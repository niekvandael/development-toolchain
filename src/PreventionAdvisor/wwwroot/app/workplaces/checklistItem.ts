import { Category } from './category';

export class ChecklistItem {
    id: string = '00000000-0000-0000-0000-000000000000';
    title: string = '';
    description: string = '';
    status: number = -1;
    categoryId: string = '';
    category: Category = new Category();
}
