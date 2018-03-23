export class Utils {
    public static isNullOrWhiteSpace(value: string): boolean {
        return value === undefined || !value || value === '' || value.trim().length === 0;
    }
}
