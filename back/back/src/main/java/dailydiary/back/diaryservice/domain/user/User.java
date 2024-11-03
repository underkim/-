package dailydiary.back.diaryservice.domain.user;

import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document(collection = "users")  // MongoDB의 'users' 컬렉션에 저장
@Data                             // getter, setter, toString, equals, hashCode 자동 생성
@NoArgsConstructor                // 기본 생성자 자동 생성
public class User {
    @Id
    private String id;             // MongoDB의 기본 ID
    private String username;       // 사용자 이름
    private String password;       // 해시된 비밀번호

    // 생성자는 Lombok의 @Data와 @NoArgsConstructor가 처리하므로 생략 가능
    public User(String username, String password) {
        this.username = username;
        this.password = password;
    }
}
